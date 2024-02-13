using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject[] enemyPreFab;

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
            int rand = Random.Range(0, enemyPreFab.Length);
            GameObject enemyToSpawn = enemyPreFab[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity); 
        }
    }
    
    void Update()
    {
        
    }
}
