using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarInitializer : MonoBehaviour
{
    public RoadArchitectSystem roadArchitectSystem;
    public List<AICarController> aiCars;

    private void Start()
    {
        List<Transform> roadNodes = roadArchitectSystem.GetRoadNodes();

        foreach (AICarController aiCar in aiCars)
        {
            WaypointSystem waypointSystem = aiCar.GetComponent<WaypointSystem>();
            if (waypointSystem != null)
            {
                waypointSystem.waypoints = roadNodes;
            }
            else
            {
                Debug.LogWarning("AICar does not have a WaypointSystem component.");
            }
        }
    }
}
