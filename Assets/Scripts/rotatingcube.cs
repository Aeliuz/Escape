using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    public Transform room;
    public bool CanPressButton = true;

    // Rotation speed in degrees per second
    public float rotationSpeed = 90f;

    // Target rotation angles for each side (in degrees)
    private Quaternion targetRotation;
    private bool isRotating = false;

    // Option to rotate continuously while holding the button
    public bool continuousRotation = false;

    // Pivot direction based on object's current front
    private Vector3 pivotDirection;

    // Audio
    private AudioSource audioSource; // The AudioSource component
    public AudioClip rotationClip; // The audio clip to be played during rotation

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the target rotation to the current rotation of the room
        targetRotation = room.rotation;
        UpdatePivotDirection();

        // Add an AudioSource component and assign the rotation sound clip
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = rotationClip;
        audioSource.loop = true; // Set it to loop if the sound should continue during rotation
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
            room.rotation = Quaternion.RotateTowards(room.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is complete
            if (Quaternion.Angle(room.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
                StopRotationSound();
            }
        }
    }

    // Update the pivot direction to always be the forward direction of the object
    void UpdatePivotDirection()
    {
        pivotDirection = transform.forward;
    }

    // Play the rotation sound when rotation starts
    void PlayRotationSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Stop the rotation sound when rotation is done or interrupted
    void StopRotationSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Rotate the room horizontally relative to the object's front direction (90 degrees)
    public void RotateHorizontally(float angle)
    {
        if (CanPressButton)
        {
            PlayRotationSound(); // Play sound on rotation start
            Vector3 axis = Vector3.up; // Use global Y-axis for horizontal rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * room.rotation;
            isRotating = true;
            CanPressButton = false;
            Invoke("AllowButtonPress", 1f);
        }
    }

    // Rotate the room vertically relative to the object's front direction (90 degrees)
    public void RotateVertically(float angle)
    {
        Debug.Log("Trying to rotate vertically");
        if (CanPressButton)
        {
            PlayRotationSound(); // Play sound on rotation start
            Debug.Log("Success");
            Vector3 axis = Vector3.right; // Use global X-axis for vertical rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * room.rotation;
            isRotating = true;
            CanPressButton = false;
            Invoke("AllowButtonPress", 1f);
        }
        else { Debug.Log("Fail"); }
    }

    // Rotate the room around another axis (overhead rotation, 90 degrees)
    public void RotateOverHead(float angle)
    {
        if (CanPressButton)
        {
            PlayRotationSound(); // Play sound on rotation start
            Vector3 axis = Vector3.forward; // Use pivot direction (object's front) for overhead rotation
            targetRotation = Quaternion.AngleAxis(angle, axis) * room.rotation;
            isRotating = true;
            CanPressButton = false;
            Invoke("AllowButtonPress", 1f);
        }
    }

    // Continuous rotation methods
    void RotateHorizontallyContinuous(float direction)
    {
        PlayRotationSound(); // Play sound on continuous rotation
        Vector3 axis = Vector3.up; // Use global Y-axis for horizontal rotation
        room.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateVerticallyContinuous(float direction)
    {
        PlayRotationSound(); // Play sound on continuous rotation
        Vector3 axis = Vector3.right; // Use global X-axis for vertical rotation
        room.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void RotateOverHeadContinuous(float direction)
    {
        PlayRotationSound(); // Play sound on continuous rotation
        Vector3 axis = pivotDirection; // Use pivot direction (object's front) for overhead rotation
        room.Rotate(axis, direction * rotationSpeed * Time.deltaTime, Space.World);
    }

    void AllowButtonPress()
    {
        CanPressButton = true;
    }

    // Draw a gizmo line to visualize the pivot direction
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}
