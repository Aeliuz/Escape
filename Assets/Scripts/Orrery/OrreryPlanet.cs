using System;
using System.Collections.Generic;
using UnityEngine;

public class OrreryPlanet : MonoBehaviour
{
    public static List<GameObject> orbiters = new List<GameObject>();    
    [SerializeField] float lengthOfYear;
    [SerializeField] string planetName;
    public bool inPosition { get; private set; } = false;
    public static event Action PlanetAdded;

    public float rotationRate()
    {
        return 360 / lengthOfYear;
    }

    private void Start()
    {
        foreach(GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            orbiters.Add(planet);
        }
    }

    public void AddedPlanet()                       // This function is called by a Unity Event when a planet is snapped to an orbit
    {
        foreach (GameObject orbiter in orbiters)
        {
            if (Math.Abs(orbiter.transform.position.x - this.gameObject.transform.GetChild(0).position.x) < 0.05
                && Math.Abs(orbiter.transform.position.z - this.gameObject.transform.GetChild(0).position.z) < 0.05
                && orbiter.gameObject.name == planetName)
                    inPosition = true;
        }
        PlanetAdded?.Invoke();
    }

    public void RemovedPlanet()                     // This function is called by a Unity Event when a planet is removed from an orbit
    {
        inPosition = false;
    }
}
