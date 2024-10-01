using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRoom : MonoBehaviour
{
    [Range(0.5f, 2f)]
    public float roomScale = 1f;
    public float roomScaleCoefficient;

    public Transform room;
    public Transform eyePosition;

    // Start is called before the first frame update
    void Start()
    {
        // roomScaleCoefficient = eyePosition.position.y;

        room.localScale = new Vector3 (roomScale, roomScale, roomScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
