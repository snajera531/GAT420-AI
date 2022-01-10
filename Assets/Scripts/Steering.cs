using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] float wanderDistance = 1;
    [SerializeField] float wanderRadius = 3;
    [SerializeField] float wanderDisplacement = 5;
    float wanderAngle = 0;

    public Vector3 Seek(AutoAgent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, target.transform.position - agent.transform.position);
        return force;
    }

    public Vector3 Flee(AutoAgent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, agent.transform.position - target.transform.position);
        return force;
    }

    Vector3 CalculateSteering(AutoAgent agent, Vector3 vector)
    {
        Vector3 direction = vector.normalized;
        Vector3 desired = direction * agent.maxSpeed;
        Vector3 steer = desired - agent.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

        return force;
    }
}
