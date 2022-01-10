using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Steering steering;
    public float maxSpeed;
    public float maxForce;

    public Vector3 Velocity { get; set; } = Vector3.zero;

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position, Color.red);

            Vector3 force = steering.Flee(this, gameObjects[0]);
            acceleration += force;
        } else
        {
            acceleration = Vector3.zero;
            Velocity = Vector3.zero;
        }

        Velocity += acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
        transform.position += Velocity * Time.deltaTime;

        if(Velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }
    }
}
