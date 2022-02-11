using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Owner.movement.Resume();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        Owner.movement.MoveTowards(Owner.enemy.transform.position);
    }
}
