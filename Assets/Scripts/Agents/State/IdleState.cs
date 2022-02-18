using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Owner.movement.Stop();
        Owner.timer.value = 2;
        Debug.Log("idle enter");
    }

    public override void OnExit()
    {
        Debug.Log("idle exit");
    }

    public override void OnUpdate()
    {
        Owner.timer.value -= Time.deltaTime;

        if (Owner.timer <= 0)
        {
            Owner.stateMachine.SetState(Owner.stateMachine.StateFromName("patrol"));
        }
    }
}
