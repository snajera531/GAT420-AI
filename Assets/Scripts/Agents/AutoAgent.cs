using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAgent : Agent
{
    [SerializeField] Perception perception;

    public Vector3 Velocity { get; set; } = Vector3.zero;

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position, Color.magenta);

            Vector3 force = transform.position - gameObjects[0].transform.position;
            acceleration += force.normalized * 3;
        } else
        {
            acceleration = Vector3.zero;
            Velocity = Vector3.zero;
        }

        Velocity += acceleration * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;
    }
}
