using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : PlayerExperience
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private bool bossSpawned = false;

    void Start()
    {   
        base.Update();
    }

    protected override void Update()
    {
        Spawner();

    }

    private void Spawner()
    {
        if (!bossSpawned)
        {
            GameObject enemyToSpawn = null;
            if (_currentLevel >= 5)
            {
                Debug.Log("Da spawnboss");
                enemyToSpawn = enemyPrefabs[0];
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                bossSpawned = true;
            }
        }
    }
}
