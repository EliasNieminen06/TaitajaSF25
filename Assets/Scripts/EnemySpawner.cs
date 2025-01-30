using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public float spawnAreaWidth = 48f;
    public float spawnAreaHeight = 22f;
    public float minDistanceFromPlayer = 5f;
    public Transform playerTransform;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool isValidPosition = false;
        while (!isValidPosition)
        {

            spawnPosition = new Vector3(Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f), 0f, Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f));
            if (Vector3.Distance(spawnPosition, playerTransform.position) >= minDistanceFromPlayer)
            {
                isValidPosition = true;
            }
        }

        return spawnPosition;
    }
}
