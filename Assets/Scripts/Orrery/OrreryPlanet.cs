using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] float lengthOfYear;
    public float initialRotation = 0f;

    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }
}
