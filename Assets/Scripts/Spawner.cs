using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField, Range(0, 10)] 
    private float spawnInterval = 5.0f;
    [SerializeField, Range(0, 10)]
    private int maxSpawn;
    [SerializeField] private int spawnCount = 0;
    private float elapsedTime;

    void Update()
    {
        if (spawnCount < maxSpawn)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= spawnInterval)
            {
                SpawnEnemy();
                spawnCount++;
                elapsedTime = 0;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyInstance = Instantiate(enemy, RandomPosition(), Quaternion.identity);
    }

    private Vector3 RandomPosition()
    {
        float randomX = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float randomZ = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

        return randomPosition;
    }
}
