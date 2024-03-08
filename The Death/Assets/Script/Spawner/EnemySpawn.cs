using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : PlayerExperience
{
    [SerializeField] private float SkeletonSpawnRate = 2f;
    [SerializeField] private float GoblinSpawnRate = 4f;

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
                if(_currentLevel < 10)
                {
                    GameObject enemyToSpawn = enemyPrefabs[0];
                    Instantiate(enemyToSpawn, spawnTransform.position, Quaternion.identity);
                }
                if(_currentLevel >= 10)
                {
                    GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                    Instantiate(enemyToSpawn, spawnTransform.position, Quaternion.identity);

                    if (enemyToSpawn == enemyPrefabs[1])
                    {
                        wait = new WaitForSeconds(GoblinSpawnRate);
                    }
                    else
                    {
                        wait = new WaitForSeconds(SkeletonSpawnRate);
                    }
                }

            }
            else
            {
                Debug.LogWarning("Failed to spawn enemy. No valid spawn point.");
            }
        }
    }
}
