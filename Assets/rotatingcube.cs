using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 90f;

    // Target rotation angles for each side (in degrees)
    private Quaternion targetRotation;
    private bool isRotating = false;

    // Reference to the VR camera or player object
    public Transform playerTransform;

    // Option to rotate continuously while holding the button
    public bool continuousRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the target rotation to the current rotation
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate horizontally or vertically based on input and player's view direction
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateHorizontally(-90);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateHorizontally(90);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            RotateVertically(-90);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            RotateVertically(90);
        }

        // Rotate around another axis (over your head) using Q and E
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateOverHead(-90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateOverHead(90);
        }

        // If continuous rotation is enabled, rotate while holding the keys
        if (continuousRotation)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                RotateHorizontallyContinuous(-1);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                RotateHorizontallyContinuous(1);
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                RotateVerticallyContinuous(-1);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                RotateVerticallyContinuous(1);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                RotateOverHeadContinuous(-1);
            }
            if (Input.GetKey(KeyCode.E))
            {
                RotateOverHeadContinuous(1);
            }
        }

        // Smoothly rotate towards the target rotation if not in continuous mode
        if (isRotating && !continuousRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is complete
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }

    // Rotate the cube horizontally relative to the player's view direction (90 degrees)
    void RotateHorizontally(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = playerTransform.up; // Rotate around the Y axis (world up)
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Rotate the cube vertically relative to the player's view direction (90 degrees)
    void RotateVertically(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = playerTransform.right; // Rotate around the X axis (player's right direction)
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Rotate the cube around another axis (overhead rotation, 90 degrees)
    void RotateOverHead(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = playerTransform.forward; // Rotate around the Z axis (player's forward direction)
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Continuous rotation methods
    void RotateHorizontallyContinuous(float direction)
    {
        Vector3 axis = playerTransform.up; // Rotate around the Y axis (world up)
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateVerticallyContinuous(float direction)
    {
        Vector3 axis = playerTransform.right; // Rotate around the X axis (player's right direction)
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateOverHeadContinuous(float direction)
    {
        Vector3 axis = playerTransform.forward; // Rotate around the Z axis (player's forward direction)
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }
}
