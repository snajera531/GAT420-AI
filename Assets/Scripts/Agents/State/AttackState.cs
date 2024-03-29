using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Owner.GetComponent<AgentDamage>().Damage();
        Owner.movement.Stop();
        Owner.animator.SetTrigger("Punch");
        Owner.timer.value = 1;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
