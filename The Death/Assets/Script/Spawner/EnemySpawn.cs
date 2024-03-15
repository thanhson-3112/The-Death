using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : PlayerExperience
{
    [SerializeField] private float SkeletonSpawnRate = 2f;
    [SerializeField] private float GoblinSpawnRate = 8f;
    [SerializeField] private float ArcherSpawnRate = 10f;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SpawnPoint spawnPoint;

    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner());
        base.Update();
    }

    /*protected override void Update()
    {
        base.Update();
    }*/

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(SkeletonSpawnRate);

        while (canSpawn)
        {
            yield return wait;

            Transform spawnTransform = spawnPoint.GetRandomPoint();
            if (spawnTransform != null)
            {
                GameObject enemyToSpawn = null;

                if (_currentLevel < 5)
                {
                    enemyToSpawn = enemyPrefabs[0];
                }
                else if (_currentLevel >= 5 && _currentLevel < 7)
                {
                    enemyToSpawn = enemyPrefabs[Random.Range(0, 2)];
                }
                else if (_currentLevel >= 7)
                {
                    enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                }

                // Instantiate enemyToSpawn
                Instantiate(enemyToSpawn, spawnTransform.position, Quaternion.identity);

                // Ch?n th?i gian ch? sau m?i l?n spawn
                wait = GetSpawnWaitTime(enemyToSpawn);
            }
        }
    }

    WaitForSeconds GetSpawnWaitTime(GameObject enemyToSpawn)
    {
        if (enemyToSpawn == enemyPrefabs[1])
        {
            return new WaitForSeconds(GoblinSpawnRate);
        }
        else if (enemyToSpawn == enemyPrefabs[0])
        {
            return new WaitForSeconds(SkeletonSpawnRate);
        }
        else
        {
            return new WaitForSeconds(ArcherSpawnRate);
        }
    }
}
