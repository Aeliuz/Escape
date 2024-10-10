using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{
    public OVRManager OVRManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ReCenter", 5f);
    }

    void ReCenter()
    {
        OVRManager.display.RecenterPose();
    }

}
