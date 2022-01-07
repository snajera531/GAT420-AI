using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    [SerializeField] float radius;
    [SerializeField] float maxAngle;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in colliders)
        {
            if (tagName == "" || col.CompareTag(tagName))
            {
                result.Add(col.gameObject);
            }
        }

        return result.ToArray();
    }
}
 