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

        // Smoothly rotate towards the target rotation
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is complete
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }

    // Rotate the cube horizontally relative to the player's view direction
    void RotateHorizontally(float angle)
    {
        if (!isRotating)
        {
            // Rotate around the world up vector, based on the player's forward direction
            Vector3 axis = playerTransform.up; // Rotate around the Y axis (world up)
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }

    // Rotate the cube vertically relative to the player's view direction
    void RotateVertically(float angle)
    {
        if (!isRotating)
        {
            // Rotate around the player's right vector
            Vector3 axis = playerTransform.right; // Rotate around the X axis (player's right direction)
            targetRotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            isRotating = true;
        }
    }
}
