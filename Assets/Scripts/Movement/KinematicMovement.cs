using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMovement : Movement
{
    private void LateUpdate()
    {
        Velocity += Acceleration * Time.deltaTime;
        float speed = Velocity.magnitude;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        Velocity = Velocity.normalized * speed;
        transform.position += Velocity * Time.deltaTime;

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));

        if (movementData.orientToMovement && Acceleration.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }

        Acceleration = Vector3.zero;
    }

    public override void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        ApplyForce(direction * maxForce);
    }

    public override void ApplyForce(Vector3 force)
    {
        Acceleration += force;
    }

    public override void Stop()
    {
        Velocity = Vector3.zero;
    }

    public override void Resume()
    {
        //
    }

}
