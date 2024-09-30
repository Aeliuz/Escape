using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    // Define a UnityEvent to be invoked when the dice lands on a side
    public UnityEvent<float> onLanded;

    private float landedValue; // Private float to store the value of the landed side
    private bool isLanded; // Flag to prevent multiple land notifications

    private void OnCollisionEnter(Collision collision)
    {
        // Get the contact point
        ContactPoint contact = collision.contacts[0];

        // Calculate the normal of the collision
        Vector3 normal = contact.normal;

        // Check which side was hit based on the normal
        landedValue = DetermineHitSide(normal);
        Debug.Log($"Hit from: {landedValue}");

        // Start the coroutine to wait before notifying subscribers
        if (!isLanded) // Prevent triggering multiple times
        {
            StartCoroutine(NotifyLandedCoroutine());
        }
    }

    IEnumerator NotifyLandedCoroutine()
    {
        isLanded = true; // Set the landed flag to true

        // Wait for 0.2 seconds (or whatever duration you prefer)
        yield return new WaitForSeconds(0.2f);

        // Invoke the event to notify subscribers
        onLanded?.Invoke(landedValue);

        // Reset the landed flag after notifying
        isLanded = false;
    }

    private float DetermineHitSide(Vector3 normal)
    {
        // Use Vector3.Dot to determine the direction of the normal
        float upDot = Vector3.Dot(normal, transform.up);
        float downDot = Vector3.Dot(normal, -transform.up);
        float leftDot = Vector3.Dot(normal, -transform.right);
        float rightDot = Vector3.Dot(normal, transform.right);
        float frontDot = Vector3.Dot(normal, transform.forward);
        float backDot = Vector3.Dot(normal, -transform.forward);

        // Determine which side was hit and assign the corresponding float value
        if (upDot > 0.5f)
        {
            return 3f; // Top
        }
        else if (downDot > 0.5f)
        {
            return 4f; // Bottom
        }
        else if (leftDot > 0.5f)
        {
            return 5f; // Left
        }
        else if (rightDot > 0.5f)
        {
            return 2f; // Right
        }
        else if (frontDot > 0.5f)
        {
            return 1f; // Front
        }
        else if (backDot > 0.5f)
        {
            return 6f; // Back
        }
        else
        {
            return 0f; // Unknown Direction
        }
    }
}
