using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private LifeBlossom lifeBlossom;
    [SerializeField] private float surviveDuration = 180f;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject startPanel;

    [Header("End Screen UI")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endText;
    [SerializeField] private TMP_Text endSubText;

    private float timer;
    private bool gameEnded;

    private void Awake()
    {
        Time.timeScale = 0f;

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }






    private void HandlePlayerKilled()
    {
        if (gameEnded) return;
        gameEnded= true;
        ShowEndPanel(
            "You Were Caught!",
            "An enemy reached the witch. Avoid direct contact while protecting the LifeBlossom."
        );
        Time.timeScale = 0f;
    }





    private void OnEnable()
    {
        if (lifeBlossom != null)
        {
            lifeBlossom.OnCoreDestroyed += HandleLose;
        }
        if (playerController != null)
        {
            playerController.OnPlayerKilled += HandlePlayerKilled;
        }
        
    }

    private void OnDisable()
    {
        if (lifeBlossom != null)
        {
            lifeBlossom.OnCoreDestroyed -= HandleLose;
        }
        if (playerController != null)
        {
            playerController.OnPlayerKilled -= HandlePlayerKilled;
        }
        
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
        ShowEndPanel(
            "Garden Restored!",
            "The LifeBlossom survived the enemy pressure."
        );
        Time.timeScale = 0f;
    }

    private void HandleLose()
    {
        if (gameEnded) return;

        gameEnded = true;
        ShowEndPanel(
            "The Garden Withered!",
            "The LifeBlossom was destroyed before the timer ended."
        );
        Time.timeScale = 0f;
    }

    private void ShowEndPanel(string titleMessage, string subMessage)
    {
        Debug.Log(titleMessage);

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        if (endText != null)
        {
            endText.text = titleMessage;
        }

        if (endSubText != null)
        {
            endSubText.text = subMessage;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}