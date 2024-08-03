using System.Collections.Generic;
using UnityEngine;

public class RoadArchitectSystem : MonoBehaviour
{
    public Transform nodeParent;
    public List<Transform> roadNodes;

    private void Awake()
    {
        roadNodes = new List<Transform>();

        if (nodeParent == null)
        {
            Debug.LogError("Node Parent not assigned.");
            return;
        }

        foreach (Transform child in nodeParent)
        {
            roadNodes.Add(child);
        }

        Debug.Log("Number of road nodes found: " + roadNodes.Count);
    }

    public List<Transform> GetRoadNodes()
    {
        return roadNodes;
    }
}