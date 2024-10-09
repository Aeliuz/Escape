using UnityEngine;

public class ParentFollower : MonoBehaviour
{
    public Transform childTransform; // Assign the child in the Inspector or via code

    private Vector3 initialOffset; // Offset between the parent and child

    void Start()
    {
        if (childTransform != null)
        {
            // Calculate initial offset at the start
            initialOffset = transform.position - childTransform.position;
        }
    }

    void LateUpdate()
    {
        if (childTransform != null)
        {
            // Move the parent to follow the child without affecting the child’s Rigidbody
            transform.position = childTransform.position + initialOffset;
            transform.rotation = childTransform.rotation; // Optionally, follow rotation too
        }
    }
}