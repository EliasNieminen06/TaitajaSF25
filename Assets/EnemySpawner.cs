using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    // The spawn area boundaries (you can adjust these in the inspector)
    public float spawnAreaWidth = 48f;
    public float spawnAreaHeight = 22f;

    // Minimum distance from the player to spawn enemies
    public float minDistanceFromPlayer = 5f;

    // Reference to the player object
    public Transform playerTransform;

    // Time between enemy spawns
    public float spawnInterval = 2f;

    private void Start()
    {
        // Start the spawning coroutine
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }

        // Choose a random enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // Find a valid spawn position
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the enemy at the spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool isValidPosition = false;

        // Keep trying to find a valid spawn position
        while (!isValidPosition)
        {
            // Random position within the defined spawn area
            spawnPosition = new Vector3(
                Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
                0f,  // Assuming 2D for this example, set to your desired y position for 3D
                Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
            );

            // Ensure the enemy isn't too close to the player
            if (Vector3.Distance(spawnPosition, playerTransform.position) >= minDistanceFromPlayer)
            {
                isValidPosition = true;
            }
        }

        return spawnPosition;
    }
}
