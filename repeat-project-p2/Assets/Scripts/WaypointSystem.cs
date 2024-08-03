using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    public List<Transform> waypoints;

    public List<Transform> GetWayPoints()
    {
        return waypoints;
    }
}
