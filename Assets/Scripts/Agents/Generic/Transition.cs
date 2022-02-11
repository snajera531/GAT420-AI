using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    Condition[] conditions;

    public Transition(Condition[] _conditions)
    {
        _conditions = conditions;
    }

    public bool ToTransition()
    {
        bool transition = false;
        foreach(var condition in conditions)
        {
            if (!condition.IsTrue()) return false;
        }

        return transition;
    }
}
