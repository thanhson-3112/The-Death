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
    [SerializeField] private Timer gameTimer; // Thêm bi?n tham chi?u ??n Timer

    private void Start()
    {
        InitializePools();
        StartCoroutine(Spawner());
        gameTimer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
    }

    private void InitializePools()
    {
        // Kh?i t?o pool cho t?ng lo?i quái v?t
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
        bool spawnedWaveSkeletons = false;

        while (canSpawn)
        {
            yield return wait;

            float elapsedTime = gameTimer.GetElapsedTime();
            Transform spawnTransform = spawnPoint.GetRandomPoint();

            if (elapsedTime >= 60f && !spawnedWaveSkeletons)
            {
                // Sinh ra 20 Skeleton cùng lúc
                for (int i = 0; i < 2; i++)
                {
                    GameObject skeleton = GetEnemyFromPool(EnemyType.Skeleton);
                    if (skeleton != null && spawnTransform != null)
                    {
                        skeleton.transform.position = spawnPoint.GetRandomPoint().position;
                        skeleton.transform.rotation = Quaternion.identity;
                        skeleton.SetActive(true);

                        // H?i l?i máu khi spawn
                        EnemyLifeBase enemyLifeBase = skeleton.GetComponent<EnemyLifeBase>();
                        if (enemyLifeBase != null)
                        {
                            enemyLifeBase.ResetHealth();
                        }
                    }
                }
                // danh dau da spawn
                spawnedWaveSkeletons = true;
            }


            if (spawnTransform != null)
            {
                GameObject enemyToSpawn = null;
                EnemyType enemyType = EnemyType.Skeleton; // Default enemy type

                if (elapsedTime < 30f)
                {
                    enemyType = EnemyType.Skeleton;
                }
                else if (elapsedTime >= 120f && elapsedTime <= 180f)
                {
                    int randomChoice = Random.Range(0, 2);
                    enemyType = randomChoice == 0 ? EnemyType.Skeleton : EnemyType.Goblin;
                }

                else if (elapsedTime >= 180f && elapsedTime <= 240f)
                {
                    int randomChoice = Random.Range(0, 3);
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
                }

                else if (elapsedTime >= 240f)
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
            case EnemyType.Flying:  // Thêm tr??ng h?p Flying
                return new WaitForSeconds(FlyingSpawnRate);
            default:
                return new WaitForSeconds(SkeletonSpawnRate); // Default
        }
    }

}
