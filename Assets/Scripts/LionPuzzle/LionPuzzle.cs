using Oculus.Interaction;
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
        if (AllLionsInPlace())
        {
            foreach (Lion lion in lions)
            {
                lion.gameObject.GetComponentInChildren<TouchHandGrabInteractable>().enabled = false;
                lion.gameObject.GetComponentInChildren<Grabbable>().enabled = false;
                lion.gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
