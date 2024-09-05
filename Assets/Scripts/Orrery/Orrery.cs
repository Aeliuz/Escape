using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orrery : MonoBehaviour
{
    [SerializeField] private List<OrreryPlanet> planets;
    [SerializeField] private OrreryStartButton startButton;
    [SerializeField] private float _currentYear;
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
        // Set initial rotation for each planet
        foreach (OrreryPlanet planet in planets)
        {
            planet.transform.Rotate(0f, planet.rotationRate() * (_currentYear - ((float)_targetYear)), 0f, Space.Self);
        }
    }

    private void Update()                       // Update function for debugging/test purposes only!
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ControlOrrery(_testYear);
        }
    }

    private void ControlOrrery(int setYear)
    {
        if (orreryRunning) return;
        _deltaYears = Math.Abs(setYear - (int)_currentYear);
        if (_deltaYears == 0) return;
        StartCoroutine(MovePlanets(setYear));
    }

    private IEnumerator MovePlanets(int setYear)
    {
        orreryRunning = true;
        while (setYear != _currentYear)
        {
            if (_currentYear < setYear)
            {
                foreach(OrreryPlanet planet in planets)
                {
                    planet.transform.Rotate(0f, planet.rotationRate() * _deltaYears / _targetTime, 0f, Space.Self);
                }
                _currentYear += _deltaYears / _targetTime;
            }
            else if (_currentYear > setYear)
            {
                foreach (OrreryPlanet planet in planets)
                {
                    planet.transform.Rotate(0f, -planet.rotationRate() * _deltaYears / _targetTime, 0f, Space.Self);
                }
                _currentYear -= _deltaYears / _targetTime;
            }
            if (Math.Abs(setYear - _currentYear) < (0.05f))
                _currentYear = setYear;

            yield return new WaitForSeconds(0.01f);
        }
        orreryRunning = false;
        if ((int)_currentYear == _targetYear)
            OnOrreryFinished?.Invoke();
    }
}
