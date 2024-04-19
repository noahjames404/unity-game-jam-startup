using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartScreen : MonoBehaviour
{
    public void Show()
    {
        // Create a restart screen object
        GameObject restartScreen = new GameObject("RestartScreen");
        Canvas canvas = restartScreen.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        restartScreen.AddComponent<CanvasScaler>();
        restartScreen.AddComponent<GraphicRaycaster>();

        // Add a panel to cover the entire screen
        GameObject panel = new GameObject("Panel");
        panel.transform.SetParent(restartScreen.transform, false);
        RectTransform panelRect = panel.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panel.AddComponent<Image>().color = new Color(0, 0, 0, 0.5f); // Semi-transparent black

        // Add "Game Over" text
        GameObject gameOverText = new GameObject("GameOverText");
        gameOverText.transform.SetParent(panel.transform, false);
        TextMeshProUGUI gameOverTextMesh = gameOverText.AddComponent<TextMeshProUGUI>();
        gameOverTextMesh.text = "Game Over";
        gameOverTextMesh.alignment = TextAlignmentOptions.Center;
        gameOverTextMesh.fontSize = 24;
        gameOverTextMesh.color = Color.red;
        RectTransform gameOverRect = gameOverText.GetComponent<RectTransform>();
        gameOverRect.sizeDelta = new Vector2(200, 50);
        gameOverRect.anchorMin = new Vector2(0.5f, 0.7f);
        gameOverRect.anchorMax = new Vector2(0.5f, 0.7f);
        gameOverRect.anchoredPosition = Vector2.zero;

        // Add a restart button
        GameObject restartButton = new GameObject("RestartButton");
        restartButton.transform.SetParent(panel.transform, false);
        RectTransform buttonRect = restartButton.AddComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(200, 50);
        restartButton.AddComponent<Image>().color = Color.white;
        Button buttonComponent = restartButton.AddComponent<Button>();
        buttonComponent.onClick.AddListener(RestartGame); // Add listener to restart the game when clicked
        TextMeshProUGUI buttonText = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        buttonText.transform.SetParent(restartButton.transform, false);
        buttonText.text = "Restart";
        buttonText.alignment = TextAlignmentOptions.Center;
        buttonText.fontSize = 20;
        buttonText.color = Color.black;
        buttonText.rectTransform.sizeDelta = Vector2.zero;
        buttonText.rectTransform.anchorMin = Vector2.zero;
        buttonText.rectTransform.anchorMax = Vector2.one;
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
