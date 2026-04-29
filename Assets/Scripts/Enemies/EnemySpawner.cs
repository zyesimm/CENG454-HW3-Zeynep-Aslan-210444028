using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform lifeBlossom;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.6f;
    [SerializeField] private float pressureIncreaseRate = 0.05f;

    private float timer;
    private float currentSpawnInterval;

    private void Awake()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    private void Update()
    {
        IncreasePressureOverTime();

        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void IncreasePressureOverTime()
    {
        currentSpawnInterval -= pressureIncreaseRate * Time.deltaTime;
        currentSpawnInterval = Mathf.Max(currentSpawnInterval, minimumSpawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || lifeBlossom == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnemySpawner is missing required references.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.SetTarget(lifeBlossom);
    }
}