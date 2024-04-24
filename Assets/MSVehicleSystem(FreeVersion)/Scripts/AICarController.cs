using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 10.0f;
    public float rotationSpeed = 5.0f;
    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Count == 0)
            return;

        float distance = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);

        if (distance < 1)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;

        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
