using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float brakeForce = 10.0f;
    public float acceleration = 10.0f;
    public float deceleration = 5.0f;
    public float maxSpeed = 20.0f;
    public float minSpeed = 0.0f;

    private float currentSpeed = 0.0f;
    private Rigidbody rb;
    private WheelCollider[] wheels;
    private float rotation = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the new speed
        if (moveVertical > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }
        else if (moveVertical < 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, minSpeed);
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeForce * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);
            }
        }

        // Rotate the car
        rotation += rotationSpeed * Input.GetAxis("Horizontal");

        // Move the wheels
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0;
            wheel.brakeTorque = 0;

            if (moveVertical > 0)
            {
                wheel.motorTorque = speed * currentSpeed;
            }
            else if (moveVertical < 0)
            {
                wheel.brakeTorque = brakeForce;
            }

            // Update the wheel rotation
            wheel.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.deltaTime);
        rb.MoveRotation(Quaternion.Euler(0, rotation, 0));
    }
}