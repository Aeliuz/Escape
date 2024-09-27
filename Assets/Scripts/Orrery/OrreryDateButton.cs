using System.Collections.Generic;
using UnityEngine;

public class OrreryDateButton : MonoBehaviour
{
    [SerializeField] OrreryDateButton otherButton;
    private MeshRenderer dial;
    [SerializeField] private List<Material> dialFace;
    private int _currentNumber;
    [SerializeField] private bool addYears;

    private void OnEnable()
    {
        Orrery.SetOrreryStart += Setup;
    }

    private void OnDisable()
    {
        Orrery.SetOrreryStart -= Setup;
    }

    private void Awake()
    {
        dial = GetComponentInParent<MeshRenderer>();
    }
        
    private void Setup(int startYear)
    {
        switch (this.gameObject.transform.parent.name)
        {
            case "Annum":
                _currentNumber = startYear % 10;                                
                break;
            case "Decade":
                _currentNumber = (startYear % 100) / 10;                         
                break;
            case "Century":
                _currentNumber = (startYear % 1000) / 100;                      
                break;
            case "Millenium":
                _currentNumber = startYear / 1000;                              
                break;
        }
        dial.material.CopyPropertiesFromMaterial(dialFace[_currentNumber]);     // This line will run twice for each dial face ¯\_(?)_/¯
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
