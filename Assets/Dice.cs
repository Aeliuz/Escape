using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float checkDelay = 1f; // Time to check if dice hasn't moved
    public float rayLength = 1f;  // Length of the raycasts
    public LayerMask rayMask;     // Layer mask for raycast

    private Rigidbody rb;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    // Public floats for each face value
    public float upValue = 1f;
    public float downValue = 6f;
    public float forwardValue = 2f;   // Example value, change as needed
    public float backValue = 5f;      // Example value, change as needed
    public float leftValue = 3f;      // Example value, change as needed
    public float rightValue = 4f;     // Example value, change as needed

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        StartCoroutine(CheckForStoppedDice());
    }

    private IEnumerator CheckForStoppedDice()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkDelay);

            // Check if the dice has moved or rotated
            if (IsDiceStopped())
            {
                CheckDiceSide();
            }

            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }
    }

    private bool IsDiceStopped()
    {
        // Check if the dice's position and rotation haven't changed significantly
        return Vector3.Distance(transform.position, lastPosition) < 0.01f && Quaternion.Angle(transform.rotation, lastRotation) < 1f;
    }

    private void CheckDiceSide()
    {
        // Cast raycasts in all six directions from the dice to check which side is facing upwards
        RaycastHit hit;

        // UP
        if (Physics.Raycast(transform.position, transform.up, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, transform.up * rayLength, Color.green); // Green for up
            Debug.Log("Up side is facing up: " + upValue);
        }
        // DOWN
        else if (Physics.Raycast(transform.position, -transform.up, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red); // Red for down
            Debug.Log("Down side is facing up: " + downValue);
        }
        // FORWARD
        else if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue); // Blue for forward
            Debug.Log("Forward side is facing up: " + forwardValue);
        }
        // BACKWARD
        else if (Physics.Raycast(transform.position, -transform.forward, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, -transform.forward * rayLength, Color.yellow); // Yellow for backward
            Debug.Log("Backward side is facing up: " + backValue);
        }
        // LEFT
        else if (Physics.Raycast(transform.position, -transform.right, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, -transform.right * rayLength, Color.magenta); // Magenta for left
            Debug.Log("Left side is facing up: " + leftValue);
        }
        // RIGHT
        else if (Physics.Raycast(transform.position, transform.right, out hit, rayLength, rayMask))
        {
            Debug.DrawRay(transform.position, transform.right * rayLength, Color.cyan); // Cyan for right
            Debug.Log("Right side is facing up: " + rightValue);
        }
    }

    private void Update()
    {
        // Continuously draw raycasts for visualization in Update()
        Debug.DrawRay(transform.position, transform.up * rayLength, Color.green);    // Up raycast
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);     // Down raycast
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue); // Forward raycast
        Debug.DrawRay(transform.position, -transform.forward * rayLength, Color.yellow); // Backward raycast
        Debug.DrawRay(transform.position, -transform.right * rayLength, Color.magenta); // Left raycast
        Debug.DrawRay(transform.position, transform.right * rayLength, Color.cyan);  // Right raycast
    }
}
