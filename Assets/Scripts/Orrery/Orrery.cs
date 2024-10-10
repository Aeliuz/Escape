using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orrery : MonoBehaviour
{
    [SerializeField] private List<OrreryPlanet> planets;
    [SerializeField] private OrreryStartButton startButton;
    [SerializeField] private int _currentYear;
    [SerializeField] private int _targetYear;
    [SerializeField] private float _targetTime = 1500f;
    private int _deltaYears;
    public bool orreryRunning = false;
    private bool planetsInPlace = false;

    public static event Action<int> SetOrreryStart;
    public static event Action OnOrreryFinished;

    private void OnEnable()
    {
        startButton.OnStartOrrery += ControlOrrery;
        OrreryPlanet.PlanetAdded += AddedPlanet;
    }

    private void OnDisable()
    {
        startButton.OnStartOrrery -= ControlOrrery;
        OrreryPlanet.PlanetAdded -= AddedPlanet;
    }

    void Start()
    {
        // Set starting rotation for each visual planet and hide it
        foreach (OrreryPlanet planet in planets)
        {
            planet.transform.Rotate(0f, planet.rotationRate() * (_currentYear - (_targetYear)), 0f, Space.Self);
            planet.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            planet.transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;
        }
        SetOrreryStart?.Invoke(_currentYear);
    }

    private void AddedPlanet()                  // Whenever a planet is snapped to orrery, this function is called
    {
        if (!CheckPlanetAssembly()) return;
        else                                                                                                        // When all planets are in their correct locations...
        {
            foreach (OrreryPlanet orbit in planets)
            {
                orbit.gameObject.transform.GetComponentInChildren<InteractableUnityEventWrapper>().enabled = false; // ... disable events searching for grabbable planets...
            }
            foreach (GameObject grabbablePlanet in OrreryPlanet.orbiters)
            {
                Destroy(grabbablePlanet);                                                                           // ... grabbable planets are destroyed...
            }
            foreach (OrreryPlanet visualPlanet in planets)                                                          // ... and hidden non-grabbable planets are revealed 
            {
                visualPlanet.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                visualPlanet.transform.GetChild(0).GetComponent<SphereCollider>().enabled = true;
            }
            planetsInPlace = true;
        }
        
    }

    private bool CheckPlanetAssembly()
    {
        foreach (OrreryPlanet planet in planets)
        {
            if (!planet.inPosition) return false;
        }
        return true;
    }
    
    private void ControlOrrery(int setYear)
    {
        if (!planetsInPlace) return;
        if (orreryRunning) return;
        _deltaYears = setYear - _currentYear;
        if (_deltaYears == 0) return;
        StartCoroutine(MovePlanets(setYear));
    }

    private IEnumerator MovePlanets(int setYear)
    {
        orreryRunning = true;
        int iterations = 0;
        while (iterations != _targetTime)
        {
            foreach(OrreryPlanet planet in planets)
            {
                planet.transform.Rotate(0f, planet.rotationRate() * _deltaYears / _targetTime, 0f, Space.Self);
            }
            iterations++;
            if (iterations == _targetTime)
                _currentYear = setYear;
            
            yield return new WaitForSeconds(0.01f);
        }
        orreryRunning = false;
        if (_currentYear == _targetYear)
            OnOrreryFinished?.Invoke();
    }
}
