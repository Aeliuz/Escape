using System.Collections.Generic;
using UnityEngine;

public class OrreryDateButton : MonoBehaviour
{
    [SerializeField] OrreryDateButton otherButton;
    private MeshRenderer dial;
    //[SerializeField] private Material liveMaterial;
    [SerializeField] private List<Material> dialFace;
    private int _currentNumber;
    [SerializeField] private bool addYears;

    void Start()
    {
        dial = GetComponentInParent<MeshRenderer>();
        _currentNumber = 0;
        dial.material.CopyPropertiesFromMaterial(dialFace[_currentNumber]);
    }

    public int GetCurrentNumber()
    {
        return _currentNumber;
    }

    public void SetCurrentNumber(int currentNumber)
    {
        _currentNumber = currentNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (addYears)
                _currentNumber++;
            else
                _currentNumber--;
            otherButton.SetCurrentNumber(_currentNumber);
            dial.material.CopyPropertiesFromMaterial(dialFace[_currentNumber]);
        }
    }
}
