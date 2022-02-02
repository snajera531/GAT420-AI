using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateAgent Owner { get; private set; }
    public string Name { get; private set; }

    public State(StateAgent _owner, string _name)
    {
        Owner = _owner;
        Name = _name;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
}
