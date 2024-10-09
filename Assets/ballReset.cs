using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class ballReset : MonoBehaviour
{
    public GameObject ball;

    public void Reset()
    {
        ball.GetComponent<SnapInteractor>().InjectOptionaTimeOut(0.1f);
        Invoke("ResetTimer", 0.4f);
    }

    void ResetTimer()
    {
        ball.GetComponent<SnapInteractor>().InjectOptionaTimeOut(120f);
    }

}
