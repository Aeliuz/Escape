using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaleRoom : MonoBehaviour
{
    [Range(0.5f, 2f)]
    public float roomScale = 1f;
    public float roomScaleCoefficient;

    public Transform room;
    public Transform eyePosition;

    float originalScale = 1.6f;
    float originalHeight = 1.82f;
    float heightToEyeHeight = 1.07f;



    void Start()
    {
        StartCoroutine(Resize());
    }

    IEnumerator Resize()
    {
        yield return new WaitForSeconds(1f);

        roomScaleCoefficient = eyePosition.position.y;

        float newScaleCoefficient = (roomScaleCoefficient * heightToEyeHeight) / originalHeight;
        float newScale = originalScale * newScaleCoefficient;

        room.localScale = new Vector3 (newScale, newScale, newScale);

        Debug.Log("1: " + roomScaleCoefficient + ", 2: " + newScaleCoefficient + ", 3: " + newScale, eyePosition);
    }

    public void RestartScene ()
    {
        SceneManager.LoadScene(0);
    }
}
