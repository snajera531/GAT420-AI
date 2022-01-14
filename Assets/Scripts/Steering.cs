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

    public Vector3 Wander(AutoAgent agent)
    {
        wanderAngle += Random.Range(-wanderDisplacement, wanderDisplacement);
        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * wanderRadius);
        Vector3 forward = agent.transform.forward * wanderDistance;

        Vector3 force = CalculateSteering(agent, (forward + point));
        return force;
    }

    public Vector3 Cohesion(AutoAgent agent, GameObject[] targets)
    {
        Vector3 centerOfTargets = Vector3.zero;
        foreach(GameObject target in targets)
        {
            centerOfTargets += target.transform.position;
        }

        centerOfTargets /= targets.Length;

        Vector3 force = CalculateSteering(agent, centerOfTargets - agent.transform.position);
        return force;
    }

    public Vector3 Separation(AutoAgent agent, GameObject[] targets, float radius)
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject target in targets)
        {
            Vector3 direction = (agent.transform.position - target.transform.position);
            if (direction.magnitude < radius)
            {
                separation += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = CalculateSteering(agent, separation);
        return force;
    }

    public Vector3 Alignment(AutoAgent agent, GameObject[] targets)
    {
        Vector3 averageVelocity = Vector3.zero;
        foreach (GameObject target in targets)
        {
            averageVelocity += target.GetComponent<AutoAgent>().Velocity;
        }

        averageVelocity /= targets.Length;

        Vector3 force = CalculateSteering(agent, averageVelocity);
        return force;
    }

    public Vector3 CalculateSteering(AutoAgent agent, Vector3 vector)
    {
        Vector3 direction = vector.normalized;
        Vector3 desired = direction * agent.maxSpeed;
        Vector3 steer = desired - agent.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

        return force;
    }
}
