using UnityEngine;
using TMPro;
using System.Collections;

public static class TextManager
{
    public static TextMeshProUGUI CreateTextMeshPro(string text, Vector3 position, Transform parent, float displaySeconds)
    {
        //create a new GameObject
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.SetParent(parent); // Set the parent transform

        TextMeshProUGUI textMeshPro = textObject.AddComponent<TextMeshProUGUI>();

        // Set text + settings
        textMeshPro.text = text;
        textObject.transform.localPosition = position;
        textMeshPro.enableWordWrapping = false;
        textMeshPro.color = Color.red;

        // Start coroutine to destroy after displaySeconds
        MonoBehaviour coroutineRunner = parent.GetComponent<MonoBehaviour>();
        coroutineRunner.StartCoroutine(DestroyTextMeshProAfterDelay(textObject, displaySeconds));

        return textMeshPro;
    }

    private static IEnumerator DestroyTextMeshProAfterDelay(GameObject textObject, float displaySeconds)
    {
        yield return new WaitForSeconds(displaySeconds);

        GameObject.Destroy(textObject);
    }
}
