using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float SkeletonSpawnRate = 3f;
    [SerializeField] private float GoblinSpawnRate = 8f;
    [SerializeField] private float ArcherSpawnRate = 20f;
    [SerializeField] private float FlyingSpawnRate = 30f;

    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject flyingPrefab;


    [SerializeField] private SpawnPoint spawnPoint;

    private bool canSpawn = true;
    [SerializeField] private Timer gameTimer; // Th�m bi?n tham chi?u ??n Timer


    private void Start()
    {
        InitializePools();
        StartCoroutine(Spawner());
        gameTimer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
    }

    private void InitializePools()
    {
        // Kh?i t?o pool cho t?ng lo?i qu�i v?t
        EnemyPool.Instance.CreatePool(EnemyPool.Instance.skeletonPool, skeletonPrefab, EnemyPool.Instance.skeletonPoolSize);
        EnemyPool.Instance.CreatePool(EnemyPool.Instance.goblinPool, goblinPrefab, EnemyPool.Instance.goblinPoolSize);
        EnemyPool.Instance.CreatePool(EnemyPool.Instance.archerPool, archerPrefab, EnemyPool.Instance.archerPoolSize);
        EnemyPool.Instance.CreatePool(EnemyPool.Instance.flyingPool, archerPrefab, EnemyPool.Instance.flyingPoolSize);
    }


    private GameObject GetEnemyFromPool(EnemyType type)
    {
        return EnemyPool.Instance.GetEnemy(type);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(SkeletonSpawnRate);
        bool spawned20Skeletons = false;

        while (canSpawn)
        {
            yield return wait;

            float elapsedTime = gameTimer.GetElapsedTime();
            Transform spawnTransform = spawnPoint.GetRandomPoint();

            if (elapsedTime >= 30f && !spawned20Skeletons)
            {
                // Sinh ra 20 Skeleton c�ng l�c
                for (int i = 0; i < 20; i++)
                {
                    GameObject skeleton = GetEnemyFromPool(EnemyType.Skeleton);
                    if (skeleton != null && spawnTransform != null)
                    {
                        skeleton.transform.position = spawnPoint.GetRandomPoint().position;
                        skeleton.transform.rotation = Quaternion.identity;
                        skeleton.SetActive(true);

                        // H?i l?i m�u khi spawn
                        EnemyLifeBase enemyLifeBase = skeleton.GetComponent<EnemyLifeBase>();
                        if (enemyLifeBase != null)
                        {
                            enemyLifeBase.ResetHealth();
                        }
                    }
                }
                // ?�nh d?u ?� spawn 20 Skeletons
                spawned20Skeletons = true;
            }


            if (spawnTransform != null)
            {
                GameObject enemyToSpawn = null;
                EnemyType enemyType = EnemyType.Skeleton; // Default enemy type

                if (elapsedTime < 30f)
                {
                    enemyType = EnemyType.Skeleton;
                }
                else if (elapsedTime >= 60f && elapsedTime <= 70f)
                {
                    int randomChoice = Random.Range(0, 2);
                    enemyType = randomChoice == 0 ? EnemyType.Skeleton : EnemyType.Goblin;
                }
                else if (elapsedTime >= 70f)
                {
                    int randomChoice = Random.Range(0, 4);
                    if (randomChoice == 0)
                    {
                        enemyType = EnemyType.Skeleton;
                    }
                    else if (randomChoice == 1)
                    {
                        enemyType = EnemyType.Goblin;
                    }
                    else if (randomChoice == 2)
                    {
                        enemyType = EnemyType.Archer;
                    }
                    else
                    {
                        enemyType = EnemyType.Flying;  
                    }
                }

                enemyToSpawn = GetEnemyFromPool(enemyType);

                if (enemyToSpawn != null)
                {
                    enemyToSpawn.transform.position = spawnTransform.position;
                    enemyToSpawn.transform.rotation = Quaternion.identity;
                    enemyToSpawn.SetActive(true);

                    // Hoi lai mau khi spawn
                    EnemyLifeBase enemyLifeBase = enemyToSpawn.GetComponent<EnemyLifeBase>();
                    if (enemyLifeBase != null)
                    {
                        enemyLifeBase.ResetHealth();
                    }
                }

                wait = GetSpawnWaitTime(enemyType);
            }
        }
    }

    private WaitForSeconds GetSpawnWaitTime(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Skeleton:
                return new WaitForSeconds(SkeletonSpawnRate);
            case EnemyType.Goblin:
                return new WaitForSeconds(GoblinSpawnRate);
            case EnemyType.Archer:
                return new WaitForSeconds(ArcherSpawnRate);
            case EnemyType.Flying:  // Th�m tr??ng h?p Flying
                return new WaitForSeconds(FlyingSpawnRate);
            default:
                return new WaitForSeconds(SkeletonSpawnRate); // Default
        }
    }
}
