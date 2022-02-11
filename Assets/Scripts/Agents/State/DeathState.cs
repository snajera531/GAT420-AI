using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Owner.movement.Stop();
        Owner.animator.SetTrigger("Death");
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
