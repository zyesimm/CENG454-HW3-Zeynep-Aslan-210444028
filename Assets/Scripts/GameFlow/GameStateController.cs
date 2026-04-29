using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private LifeBlossom lifeBlossom;
    [SerializeField] private float surviveDuration = 180f;

    private float timer;
    private bool gameEnded;

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
        gameEnded = true;
        Debug.Log("Garden Restored! You Win!");
        Time.timeScale = 0f;
    }

    private void HandleLose()
    {
        gameEnded = true;
        Debug.Log("The Garden Withered! You Lose!");
        Time.timeScale = 0f;
    }
}