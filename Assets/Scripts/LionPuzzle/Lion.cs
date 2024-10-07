using System;
using UnityEngine;

public class Lion : MonoBehaviour
{
    public bool inPosition()
    {
        return (Math.Abs(this.gameObject.transform.localPosition.x) < 0.05f &&
                Math.Abs(this.gameObject.transform.localPosition.z) < 0.05f);
    }
}
