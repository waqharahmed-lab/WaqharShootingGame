using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    private float zombieCount;
    public int waveNumber;

    void Start()
    {
        // Spawn the first wave
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        // Count how many zombies are currently active
        zombieCount = FindObjectsOfType<zombieMove>().Length;

        // If all zombies are destroyed, spawn a new wave
        if (zombieCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int numberOfZombies)
    {
         // how many zombies per wave

        for (int i = 0; i < numberOfZombies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
