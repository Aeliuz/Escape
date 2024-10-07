using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButtons : MonoBehaviour
{
    [SerializeField] GameObject hinge;
    [SerializeField] private int boxOpenIterations;
    [SerializeField] int[] correctSequence = { 1, 2, 3, 4 };
    int sequenceNumber = 0;
    int currentButtonPress;
    private bool boxOpen = false;

    public static event Action LockBoxOpen;

    public void ButtonPress(int keyNumber)
    {
        if (boxOpen) return;
        if (keyNumber == correctSequence[sequenceNumber])
        {
            if (sequenceNumber == correctSequence.Length - 1)
            {
                StartCoroutine(UnBoxing());
                boxOpen = true;
                LockBoxOpen?.Invoke();
            }
            else sequenceNumber++;
        }
        else sequenceNumber = 0;
    }

    private IEnumerator UnBoxing()
    {
        hinge.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        float hingeRotation = -100 / boxOpenIterations;
        int iterations = 0;
        while (iterations < boxOpenIterations)
        {
            hinge.transform.Rotate(0f, hingeRotation, 0f, Space.Self);
            iterations++;
            yield return new WaitForFixedUpdate();
        }
    }
}
