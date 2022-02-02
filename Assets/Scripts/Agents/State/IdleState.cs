using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateAgent _owner, string _name) : base(_owner, _name) { }

    public override void OnEnter()
    {
        Debug.Log(Name + " enter");
    }

    public override void OnExit()
    {
        Debug.Log(Name + " exit");
    }

    public override void OnUpdate()
    {
        Debug.Log(Name + " update");
    }
}
