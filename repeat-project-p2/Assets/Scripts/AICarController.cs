using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public RoadArchitectSystem roadArchitectSystem;
    public float speed = 10f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float rotationSpeed = 5f;
    public float waypointThreshold = 2f;
    public float obstacleDetectionRange = 5f;
    public float avoidanceStrength = 10f;

    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;

    private void Start()
    {
        waypoints = roadArchitectSystem.GetRoadNodes();

        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints found for AI Car");
        }
    }

    private void Update()
    {
        if (waypoints.Count == 0)
            return;

        if (!AvoidObstacles())
        {
            MoveTowardsWaypoint();
        }
        else
        {
            Decelerate();
        }
    }

    private bool AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDetectionRange))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle detected: " + hit.collider.name);
                Vector3 avoidDirection = Vector3.Reflect(transform.forward, hit.normal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidDirection), rotationSpeed * Time.deltaTime);
                return true;
            }
        }
        return false;
    }

    private void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0;

        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointThreshold)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0)
                currentSpeed = 0;
        }
        else
        {
            if (currentSpeed < speed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }

        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        Vector3 futurePosition = transform.position + transform.forward * 2f;
        Vector3 futureDirection = targetWaypoint.position - futurePosition;
        Quaternion targetRotation = Quaternion.LookRotation(futureDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }

        Debug.Log("Current Speed: " + currentSpeed + " | Waypoint: " + currentWaypointIndex);
    }

    private void Decelerate()
    {
        currentSpeed -= deceleration * Time.deltaTime;
        if (currentSpeed < 0)
            currentSpeed = 0;

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
}
