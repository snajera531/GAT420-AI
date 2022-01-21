using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : Node
{
    public WaypointNode[] nextWaypoints;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<WaypointAgent>(out WaypointAgent searchAgent))
        {
            if(searchAgent.targetNode == this)
            {
                searchAgent.targetNode = nextWaypoints[Random.Range(0, nextWaypoints.Length)];
            }
        }
    }

    public static WaypointNode GetRandomWaypoint()
    {
        var waypoints = GetNodes<WaypointNode>();

        return (waypoints == null) ? null : waypoints[Random.Range(0, waypoints.Length)];
    }

}
