using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemiesParent;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private int maxEnemyAmount = 5; // max amount of enemies that can coexist at once
    [SerializeField] private int spawnDistance = 50;
    [SerializeField] private float spawnRate = 10.0f; // seconds
    public float nextEnemySpawnTimer = 2.0f; // wait time before spawning the next enemy

    private void Awake()
    {
        enemiesParent = GameObject.Find("Enemies");
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // limit the number of enemies on screen
        if(enemiesParent.transform.childCount <= maxEnemyAmount)
        {
            nextEnemySpawnTimer -= Time.deltaTime; // count down to next enemy spawn

            if (nextEnemySpawnTimer <= 0)
            {
                nextEnemySpawnTimer = spawnRate; // reset timer
                spawnRate *= 0.9f; // increase spawn rate by 10% each time

                int randomEnemy = Random.Range(0, enemyPrefab.Length); // random enemy
                Vector3 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;

                Instantiate(enemyPrefab[randomEnemy], spawnPosition, Quaternion.identity, enemiesParent.transform);
            }
        }
    }
}