using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    public RoamState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(-90, 90), 0);
        //"set the forward vector by rotating the owner transform forward with the quaternion rotation"
        //maple pls what is this
        Vector3 forward = Vector3.zero;
        //Vector3 forward = Vector3.RotateTowards(Vector3.forward, Owner.transform.position, rotation, );
        Vector3 destination = Owner.transform.position + forward * Random.Range(10f, 15f);

        Owner.movement.MoveTowards(destination);
        Owner.movement.Resume();
        Owner.atDestination.value = false;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if(Vector3.Distance(Owner.transform.position, Owner.movement.Destination) <= 1.5)
        {
            Owner.atDestination.value = true;
        }
    }
}
