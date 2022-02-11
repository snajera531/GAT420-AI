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
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
