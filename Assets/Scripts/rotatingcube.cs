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

    // Option to rotate continuously while holding the button
    public bool continuousRotation = false;

    // Pivot direction based on object's current front
    private Vector3 pivotDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the target rotation to the current rotation
        targetRotation = transform.rotation;
        UpdatePivotDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the pivot direction
        UpdatePivotDirection();

        // Rotate horizontally or vertically based on input and pivot direction
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

    // Update the pivot direction to always be the forward direction of the object
    void UpdatePivotDirection()
    {
        pivotDirection = transform.forward;
    }

    // Rotate the cube horizontally relative to the object's front direction (90 degrees)
    void RotateHorizontally(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = Vector3.up; // Use global Y-axis for horizontal rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Rotate the cube vertically relative to the object's front direction (90 degrees)
    void RotateVertically(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = Vector3.right; // Use global X-axis for vertical rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Rotate the cube around another axis (overhead rotation, 90 degrees)
    void RotateOverHead(float angle)
    {
        if (!isRotating || continuousRotation)
        {
            Vector3 axis = pivotDirection; // Use pivot direction (object's front) for overhead rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Continuous rotation methods
    void RotateHorizontallyContinuous(float direction)
    {
        Vector3 axis = Vector3.up; // Use global Y-axis for horizontal rotation
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateVerticallyContinuous(float direction)
    {
        Vector3 axis = Vector3.right; // Use global X-axis for vertical rotation
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateOverHeadContinuous(float direction)
    {
        Vector3 axis = pivotDirection; // Use pivot direction (object's front) for overhead rotation
        transform.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    // Draw a gizmo line to visualize the pivot direction
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}
