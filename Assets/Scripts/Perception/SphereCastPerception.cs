using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SphereCastPerception : Perception
{
    [SerializeField] Transform sphereCastTransform;
    [SerializeField] [Range(2, 50)] public int numSphereCast = 2;
    [SerializeField] [Range(0, 5)] public float radius;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        float angleOffset = (angle * 2) / (numSphereCast - 1);
        for (int i = 0; i < numSphereCast; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(-angle + (angleOffset * i), Vector3.up);
            Vector3 direction = rotation * sphereCastTransform.forward;
            Ray ray = new Ray(sphereCastTransform.position, direction);
            if (Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance))
            {
                if (tagName == "" || raycastHit.collider.CompareTag(tagName))
                {
                    //Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
                    result.Add(raycastHit.collider.gameObject);
                }
            }
            else
            {
                //Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
            }
        }

        return result.ToArray();
    }
}
