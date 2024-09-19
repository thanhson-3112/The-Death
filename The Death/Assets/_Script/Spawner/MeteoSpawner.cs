using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{
    [SerializeField] private float meteoSpawnRate = 4f;
    [SerializeField] private SpawnPoint spawnPoint;


    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(meteoSpawnRate);

        while (canSpawn)
        {
            yield return wait;

            Transform spawnTransform = spawnPoint.GetRandomPoint();
            if (spawnTransform != null)
            {
                GameObject meteoToSpawn = MeteoPool.Instance.GetMeteo();

                if (meteoToSpawn != null)
                {
                    meteoToSpawn.transform.position = spawnTransform.position;
                    meteoToSpawn.transform.rotation = Quaternion.identity;
                    meteoToSpawn.SetActive(true);
                }
            }


        }
    }

   

}
