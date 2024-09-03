using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    [SerializeField] float daysPerAnnum;
    public float rotationRate()
    {
        return 360 / daysPerAnnum;
    }

}
