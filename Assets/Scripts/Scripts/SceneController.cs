using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject meleeEnemyPrefab;
    [SerializeField] private GameObject rangedEnemyPrefab;
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private float spawnInterval = 6.0f; // Interval between spawns
    [SerializeField] private float increaseInterval = 35.0f; // Interval to increase spawn count

    private int spawnCount = 1; // Number of enemies to spawn each interval
    public int currentEnemyCount = 0; // Current number of enemies in the scene
    public int totalEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseSpawnCount());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < spawnCount)
            {
                SpawnEnemy();
                currentEnemyCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator IncreaseSpawnCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            spawnCount++;
        }
    }

    public void DecreaseEnemyCount() 
    {
        if (currentEnemyCount > 0)
        {
            currentEnemyCount--;
            totalEnemyCount++;

        }
    }

    private void SpawnEnemy()
    {
        // Choose a random enemy type
        GameObject enemyPrefab = Random.value < 0.5f ? meleeEnemyPrefab : rangedEnemyPrefab;

        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the enemy at the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public int GetTotalEnemyCount()
    {
        return totalEnemyCount;
    }

}