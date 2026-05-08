using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int pointsPerEnemy = 10;

    private int score;

    private void OnEnable()
    {
        EnemyEvents.OnEnemyDied += AddScore;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyDied -= AddScore;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void AddScore()
    {
        score += pointsPerEnemy;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
}