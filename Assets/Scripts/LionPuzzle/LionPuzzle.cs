using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionPuzzle : MonoBehaviour
{
    private static List<Lion> lions = new List<Lion>();
    public static event Action OnLionPuzzleComplete;

    private void Start()
    {
        foreach (GameObject lion in GameObject.FindGameObjectsWithTag("Lion"))
        {
            lions.Add(lion.GetComponent<Lion>());
        }
    }

    public void LionUpdate()        // This function is called by a Unity event whenever a lion is snapped to a pedestal
    {
        Debug.LogWarning("Lion snapped!");
        if (AllLionsInPlace())
        {
            foreach (Lion lion in lions)
            {
                lion.transform.GetChild(2).gameObject.SetActive(false);
                lion.transform.GetChild(0).gameObject.SetActive(false);
            }
            OnLionPuzzleComplete?.Invoke();
        }
    }

    private static bool AllLionsInPlace()
    {
        foreach(Lion lion in lions)
        {
            if (!lion.inPosition()) return false;
        }
        return true;
    }
}
