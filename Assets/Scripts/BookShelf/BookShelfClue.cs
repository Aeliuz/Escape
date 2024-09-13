using UnityEngine;
using TMPro;
using System.Collections;

public class BookShelfClue : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public Color textColor = Color.white;

    public void UpdateTextTexture(string text)
    {
        // Create a new TextMeshPro object
        GameObject textObject = new GameObject("TMP_Text");
        textObject.gameObject.transform.position = transform.position;

        // Add and configure TextMeshPro component
        TextMeshPro textMeshPro = textObject.AddComponent<TextMeshPro>();
        textMeshPro.transform.rotation = transform.rotation;
        textMeshPro.text = text;
        textMeshPro.font = fontAsset;
        textMeshPro.fontSize = 0.18f;
        textMeshPro.color = textColor;
        textMeshPro.alignment = TextAlignmentOptions.Center;

        // Adjust position and size if necessary
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0.18f, 0.18f); // Adjust size
    }
}
