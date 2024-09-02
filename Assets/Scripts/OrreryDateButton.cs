using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryDateButton : MonoBehaviour
{
    private Material dial;
    [SerializeField] private List<Texture3D> dialFace;
    private int _currentNumber;
    [SerializeField] private bool addYears;

    void Start()
    {
        dial = GetComponentInParent<Material>();
        _currentNumber = 0;
        dial.mainTexture = dialFace[_currentNumber];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (addYears)
                _currentNumber++;
            else
                _currentNumber--;
            dial.mainTexture = dialFace[_currentNumber];
        }
    }
}
