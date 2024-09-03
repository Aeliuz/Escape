using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryStartButton : MonoBehaviour
{
    [SerializeField] private List<OrreryDateButton> setDate;
    [SerializeField] private Orrery orrery;
    private int setYear;

    public event Action<int> OnStartOrrery;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (orrery.orreryRunning) return;
        if (other.gameObject.tag == "Player")
        {
            setYear = setDate[3].GetCurrentNumber();
            setYear += setDate[2].GetCurrentNumber() * 10;
            setYear += setDate[1].GetCurrentNumber() * 100;
            setYear += setDate[0].GetCurrentNumber() * 1000;
            OnStartOrrery?.Invoke(setYear);
        }
    }
}
