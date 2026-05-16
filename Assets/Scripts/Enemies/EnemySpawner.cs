using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform lifeBlossom;

    [Header("Spawn Area")]
    [SerializeField] private float spawnXLimit = 8f;
    [SerializeField] private float spawnYLimit = 5f;

    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnInterval = 2.5f;
    [SerializeField] private float minimumSpawnInterval = 0.75f;
    [SerializeField] private float pressureIncreaseRate = 0.015f;

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
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 || lifeBlossom == null)
        {
            Debug.LogWarning("EnemySpawner is missing required references.");
            return;
        }

        Enemy selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Vector3 spawnPosition = GetRandomEdgePosition();

        Enemy enemy = Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
        enemy.SetTarget(lifeBlossom);
    }

    private Vector3 GetRandomEdgePosition()
    {
        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0: // Left
                x = -spawnXLimit;
                y = Random.Range(-spawnYLimit, spawnYLimit);
                break;

            case 1: // Right
                x = spawnXLimit;
                y = Random.Range(-spawnYLimit, spawnYLimit);
                break;

            case 2: // Top
                x = Random.Range(-spawnXLimit, spawnXLimit);
                y = spawnYLimit;
                break;

            case 3: // Bottom
                x = Random.Range(-spawnXLimit, spawnXLimit);
                y = -spawnYLimit;
                break;
        }

        return new Vector3(x, y, 0f);
    }
}