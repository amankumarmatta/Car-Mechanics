using UnityEngine;

public class AICarController : MonoBehaviour
{
    // Waypoints for the AI car to follow
    public Transform[] waypoints;

    // Car's current state
    private float speed = 0f;
    private float steeringAngle = 0f;
    private bool isFollowingWaypoints = true;

    // Car's settings
    public float acceleration = 5f;
    public float braking = 5f;
    public float maxSpeed = 100f;
    public float minSpeed = 10f;
    public float steeringSpeed = 5f;
    public float steeringSensitivity = 10f;

    // Wheel colliders and meshes
    public WheelCollider[] wheels;
    public WheelMesh[] wheelMeshes;

    private Rigidbody rb;
    private float wheelRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        wheels = GetComponents<WheelCollider>();

        foreach (WheelCollider wheel in wheels)
        {
            // Create a mesh for the wheel
            Mesh wheelMesh = new Mesh();
            wheelMesh.vertices = new Vector3[] {
                new Vector3(-1, -1, 0),
                new Vector3(1, -1, 0),
                new Vector3(1, 1, 0),
                new Vector3(-1, 1, 0)
            };
            wheelMesh.triangles = new int[] {
                0, 1, 2,
                2, 3, 0
            };
            wheelMesh.RecalculateNormals();
            wheelMesh.RecalculateBounds();

            // Create a new MeshFilter and assign the wheel mesh to it
            MeshFilter meshFilter = wheel.GetComponent<MeshFilter>();
            meshFilter.mesh = wheelMesh;
        }
    }

    void Update()
    {
        // Check if the car is following waypoints
        if (isFollowingWaypoints)
        {
            // Calculate the direction to the next waypoint
            Vector3 direction = (waypoints[0].position - transform.position).normalized;

            // Calculate the steering angle based on the direction and speed
            float steeringInput = Mathf.Lerp(steeringAngle, direction.x, steeringSensitivity * Time.deltaTime);

            // Apply steering input
            steeringAngle = steeringInput;
            rb.AddTorque(new Vector3(steeringAngle * rb.mass * steeringSpeed * Time.deltaTime, 0, 0));

            // Accelerate or brake based on the speed
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else if (speed > minSpeed)
            {
                speed -= braking * Time.deltaTime;
            }

            // Update the car's speed
            rb.velocity = transform.forward * speed;

            // Check if the car has reached the finish line
            if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].position) < 1f)
            {
                isFollowingWaypoints = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Update the car's position
        transform.position += rb.velocity * Time.deltaTime;

        // Update the wheel rotation
        wheelRotation += speed * Time.deltaTime;
        foreach (WheelCollider wheel in wheels)
        {
            wheel.steerAngle = wheelRotation;
        }
    }

    void LateUpdate()
    {
        // Update the wheel meshes
        foreach (WheelMesh wheelMesh in wheelMeshes)
        {
            wheelMesh.UpdateMesh(wheelRotation);
        }
    }
}

// WheelMesh class to update the wheel meshes
public class WheelMesh : MonoBehaviour
{
    public Mesh mesh;
    public Material material;

    public void UpdateMesh(float rotation)
    {
        // Update the mesh vertices and triangles based on the rotation
        // ...
    }
}