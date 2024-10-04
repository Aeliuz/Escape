using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pipe Puzzle Complete");
    }
}
