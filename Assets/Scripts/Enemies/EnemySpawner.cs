using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform lifeBlossom;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnInterval = 2.5f;
    [SerializeField] private float minimumSpawnInterval = 1.2f;
    [SerializeField] private float pressureIncreaseRate = 0.02f;

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
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 || lifeBlossom == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnemySpawner is missing required references.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.z = 0f;

        Enemy enemy = Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);
        enemy.SetTarget(lifeBlossom);
    }
}