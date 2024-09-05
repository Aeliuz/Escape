using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] float lengthOfYear;

    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }
}
