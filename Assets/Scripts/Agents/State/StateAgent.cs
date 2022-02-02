using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    public StateMachine stateMachine = new StateMachine();

    void Start()
    {
        stateMachine.AddState(new IdleState(this, "idle"));
        stateMachine.AddState(new PatrolState(this, "patrol"));
        stateMachine.SetState(new IdleState(this, "idle"));
    }

    void Update()
    {
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SetState(new IdleState(this, "patrol"));

        }

        if (movement.Velocity.magnitude > 0.1f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
