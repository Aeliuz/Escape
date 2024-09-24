using UnityEngine;
using TMPro;
using System.Collections;

public class BookShelfClue : MonoBehaviour
{
    public TMP_Text noteText;

    public void UpdateTextTexture(string text)
    {
        noteText.text = text;
    }
}
