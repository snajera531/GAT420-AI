using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshMovement))]
public class NavMeshMovement : Movement
{
    public override Vector3 Destination { get => navMeshAgent.destination; set => navMeshAgent.destination = value; }
    public override Vector3 Velocity { get => navMeshAgent.velocity; set => navMeshAgent.velocity = value; }

    [SerializeField] NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.speed = movementData.maxSpeed;
        navMeshAgent.acceleration = movementData.maxForce;
        navMeshAgent.angularSpeed = movementData.turnRate;
    }

    public override void ApplyForce(Vector3 force) { }

    public override void MoveTowards(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    public override void Resume()
    {
        navMeshAgent.isStopped = false;
    }

    public override void Stop()
    {
        navMeshAgent.isStopped = true;
    }
}
