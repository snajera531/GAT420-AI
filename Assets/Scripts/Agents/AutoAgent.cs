using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Perception flockPerception;
    [SerializeField] ObstaclePerception obstaclePerception;
    [SerializeField] Steering steering;
    [SerializeField] AutoAgentData agentData;

    public float maxSpeed { get { return agentData.maxSpeed; } }
    public float maxForce { get { return agentData.maxForce; } }

    public Vector3 Velocity { get; set; } = Vector3.zero;

    void Update()
    {
        Vector3 acceleration = Vector3.zero;
        GameObject[] gameObjects = perception.GetGameObjects();

        //wandering
        if (gameObjects.Length == 0)
        {
            acceleration += steering.Wander(this);
        }

        //seek and flee
        if (gameObjects.Length != 0)
        {
            acceleration += steering.Seek(this, gameObjects[0]) * agentData.seekWeight;
            acceleration += steering.Flee(this, gameObjects[0]) * agentData.fleeWeight;
        }

        //flocking (cohesion and separation)
        gameObjects = flockPerception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            acceleration += steering.Cohesion(this, gameObjects) * agentData.cohesionWeight;
            acceleration += steering.Separation(this, gameObjects, agentData.separationRadius) * agentData.separationWeight;
            acceleration += steering.Alignment(this, gameObjects) * agentData.alignmentWeight;
        }

        //obstacle avoidance
        if (obstaclePerception.IsObstacleInFront())
        {
            Vector3 direction = obstaclePerception.GetOpenDirection();
            acceleration += steering.CalculateSteering(this, direction) * agentData.obstacleWeight;
        }

        Velocity += acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
        transform.position += Velocity * Time.deltaTime;

        if (Velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
