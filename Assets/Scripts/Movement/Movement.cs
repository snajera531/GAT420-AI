using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected MovementData movementData;

    public float minSpeed { get { return movementData.minSpeed; } }
    public float maxSpeed { get { return movementData.maxSpeed; } }
    public float maxForce { get { return movementData.maxForce; } }

    public virtual Vector3 Acceleration { get; set; } = Vector3.zero;
    public virtual Vector3 Destination { get; set; } = Vector3.zero;
    public virtual Vector3 Direction { get { return Velocity.normalized; } }
    public virtual Vector3 Velocity { get; set; } = Vector3.zero;

    public abstract void MoveTowards(Vector3 target);
    public abstract void ApplyForce(Vector3 force);
    public abstract void Stop();
    public abstract void Resume();
}
