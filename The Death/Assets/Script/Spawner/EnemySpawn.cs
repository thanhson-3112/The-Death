using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SpawnPoint spawnPoint;

    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            Transform spawnTransform = spawnPoint.GetRandomPoint();
            if (spawnTransform != null)
            {
                GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Instantiate(enemyToSpawn, spawnTransform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to spawn enemy. No valid spawn point.");
            }
        }
    }
}
