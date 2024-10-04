using System.Collections.Generic;
using UnityEngine;

public class OrreryDateButton : MonoBehaviour
{
    [SerializeField] OrreryDateButton otherButton;
    [SerializeField] private MeshRenderer dial;
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
            case "Millennium":
                _currentNumber = startYear / 1000;                              
                break;
        }
        dial.material.CopyPropertiesFromMaterial(dialFace[_currentNumber]);     // This line will run twice for each dial face ¯\_(ö)_/¯
    }

    public int GetCurrentNumber()
    {
        return _currentNumber;
    }

    public void SetCurrentNumber(int currentNumber)
    {
        _currentNumber = currentNumber;
    }

    public void ButtonPressed()
    {
        if (addYears)
        {
            _currentNumber++;
            if (_currentNumber > 9)
                _currentNumber = 0;
        }
        else
        {
            _currentNumber--;
            if (_currentNumber < 0)
                _currentNumber = 9;
        }
        otherButton.SetCurrentNumber(_currentNumber);
        dial.material.CopyPropertiesFromMaterial(dialFace[_currentNumber]);
    }
}
