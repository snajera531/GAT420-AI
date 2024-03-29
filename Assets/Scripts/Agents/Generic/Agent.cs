using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public Animator animator;
    public Movement movement;

    public static T[] GetAgents<T>() where T : Agent
    {
        return FindObjectsOfType<T>();
    }
}
