using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Timer gameTimer; 
    private bool bossSpawned = false;

    void Start()
    {   

        gameTimer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
    }

    protected void Update()
    {
        Spawner();
    }

    private void Spawner()
    {
        if (!bossSpawned)
        {
            float elapsedTime = gameTimer.GetElapsedTime();
            GameObject enemyToSpawn = null;
            if (elapsedTime >= 360f)
            {
                Debug.Log("Da spawnboss");
                enemyToSpawn = enemyPrefabs[0];
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                bossSpawned = true;
            }
        }
    }
}
