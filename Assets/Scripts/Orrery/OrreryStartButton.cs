using System;
using System.Collections.Generic;
using UnityEngine;

public class OrreryStartButton : MonoBehaviour
{
    [SerializeField] private List<OrreryDateButton> setDate;
    private int setYear;

    public event Action<int> OnStartOrrery;

   
    private void OnTriggerEnter(Collider other)
    {
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
