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

    void Update()
    {
        Vector3 acceleration = Vector3.zero;
        GameObject[] gameObjects = perception.GetGameObjects();

        //wandering
        if (movement.Acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(steering.Wander(this));
        }

        //seek and flee
        if (gameObjects.Length != 0)
        {
            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * agentData.seekWeight);
            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * agentData.fleeWeight);
        }

        //flocking (cohesion and separation)
        gameObjects = flockPerception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            movement.ApplyForce(steering.Cohesion(this, gameObjects) * agentData.cohesionWeight);
            movement.ApplyForce(steering.Separation(this, gameObjects, agentData.separationRadius) * agentData.separationWeight);
            movement.ApplyForce(steering.Alignment(this, gameObjects) * agentData.alignmentWeight);
        }

        //obstacle avoidance
        if (obstaclePerception.IsObstacleInFront())
        {
            Vector3 direction = obstaclePerception.GetOpenDirection();
            acceleration += steering.CalculateSteering(this, direction) * agentData.obstacleWeight;
        }
    }
}
