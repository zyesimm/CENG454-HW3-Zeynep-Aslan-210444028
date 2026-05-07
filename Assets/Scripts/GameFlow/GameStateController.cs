using TMPro;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private LifeBlossom lifeBlossom;
    [SerializeField] private float surviveDuration = 180f;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endText;

    private float timer;
    private bool gameEnded;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        lifeBlossom.OnCoreDestroyed += HandleLose;
    }

    private void OnDisable()
    {
        lifeBlossom.OnCoreDestroyed -= HandleLose;
    }

    private void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= surviveDuration)
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        if (gameEnded) return;

        gameEnded = true;
        ShowEndPanel("Garden Restored!");
        Time.timeScale = 0f;
    }

    private void HandleLose()
    {
        if (gameEnded) return;

        gameEnded = true;
        ShowEndPanel("The Garden Withered!");
        Time.timeScale = 0f;
    }

    private void ShowEndPanel(string message)
    {
        Debug.Log(message);

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        if (endText != null)
        {
            endText.text = message;
        }
    }
}