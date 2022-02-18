using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PatrolState : State
{
    public PatrolState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Owner.pathFollower.targetNode = Owner.pathFollower.path.GetNearestNode(Owner.transform.position);
        Owner.movement.Resume();
        Owner.timer.value = Random.Range(5, 10);

        Debug.Log("patrol enter");
    }

    public override void OnExit()
    {
        Owner.movement.Stop();

        Debug.Log("patrol exit");
    }

    public override void OnUpdate()
    {
        Owner.pathFollower.Move(Owner.movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Owner.stateMachine.SetState(Owner.stateMachine.StateFromName("idle"));
        }
    }
}
