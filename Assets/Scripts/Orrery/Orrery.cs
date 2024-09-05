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
    private int _deltaYears;
    private float _targetTime = 1500f;
    [SerializeField] private int _testYear;     // for debugging/testing only!
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
        // Set starting rotation for each planet
        foreach (OrreryPlanet planet in planets)
        {
            planet.transform.Rotate(0f, planet.rotationRate() * (_currentYear - (_targetYear)), 0f, Space.Self);
        }
    }

    private void Update()                       // Update function for debugging/test purposes only!
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ControlOrrery(_testYear);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(planets[0].transform.rotation.eulerAngles.y);
        }
    }

    private void ControlOrrery(int setYear)
    {
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
