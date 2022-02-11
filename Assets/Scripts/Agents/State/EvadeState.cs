using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    float angle;
    float distance;

    public EvadeState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        angle = Owner.perception.angle;
        distance = Owner.perception.distance;

        Owner.perception.angle = 180;
        Owner.perception.distance = 10;
        Owner.movement.Resume();

        Owner.animator.SetTrigger("Flee");
        Owner.timer.value = 1;
    }

    public override void OnExit()
    {
        Owner.perception.angle = angle;
        Owner.perception.distance = distance;
    }

    public override void OnUpdate()
    {
        Vector3 direction = Owner.transform.position - Owner.enemy.transform.position;
        Owner.movement.MoveTowards(Owner.enemy.transform.position);
    }
}
