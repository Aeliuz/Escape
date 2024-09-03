using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orrery : MonoBehaviour
{
    [SerializeField] private List<OrreryPlanet> planets;
    [SerializeField] private OrreryStartButton startButton;
    [SerializeField] public float orreryspeed = 1f;
    [SerializeField] private int _currentDate;
    [SerializeField] private int _targetDate;
    [SerializeField] private int _testDate;     // for debugging/testing only!
    //private float days = 365.26f;
    public bool orreryRunning = false;

    public event Action OnOrreryFinished;

    private void OnEnable()
    {
        startButton.OnStartOrrery += ControlOrrery;
    }

    private void OnDisable()
    {
        startButton.OnStartOrrery += ControlOrrery;
    }

    void Start()
    {
        // Set initial rotation for each planet
        foreach (OrreryPlanet planet in planets)
        {
            planet.transform.Rotate(0f, planet.rotationRate() * (_currentDate - _targetDate), 0f, Space.Self);
        }
    }

    private void Update()                       // Update function for debugging/test purposes only!
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ControlOrrery(_testDate);
        }
    }

    private void ControlOrrery(int setDate)
    {
        StartCoroutine(MovePlanets(setDate * 365));
    }

    private IEnumerator MovePlanets(int setDate)
    {
        orreryRunning = true;
        while (setDate != _currentDate)
        {
            if (_currentDate < setDate)
            {
                foreach(OrreryPlanet planet in planets)
                {
                    planet.transform.Rotate(0f, planet.rotationRate(), 0f, Space.Self);
                }
                _currentDate++;
            }
            else if (_currentDate > setDate)
            {
                foreach (OrreryPlanet planet in planets)
                {
                    planet.transform.Rotate(0f, -planet.rotationRate(), 0f, Space.Self);
                }
                _currentDate--;
            }
            
            yield return new WaitForSeconds(0.02f / orreryspeed);
        }
        orreryRunning = false;
        if (_currentDate == _targetDate)
            OnOrreryFinished?.Invoke();
    }
}
