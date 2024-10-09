using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{

    public GameObject ball;
    public GameObject button;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pipe Puzzle Complete");
        ball.GetComponent<SnapInteractor>().InjectOptionaTimeOut(-1);
        button.SetActive(false);
    }
}
