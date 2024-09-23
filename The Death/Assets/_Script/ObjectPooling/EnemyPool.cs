using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private GameObject archerPrefab;

    [SerializeField] private int skeletonPoolSize = 50;
    [SerializeField] private int goblinPoolSize = 50;
    [SerializeField] private int archerPoolSize = 50;


    public List<GameObject> skeletonPool { get; private set; }
    public List<GameObject> goblinPool { get; private set; }
    public List<GameObject> archerPool { get; private set; }


    [SerializeField] private Transform parentTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        skeletonPool = new List<GameObject>();
        goblinPool = new List<GameObject>();
        archerPool = new List<GameObject>();

        CreatePool(skeletonPool, skeletonPrefab, skeletonPoolSize);
        CreatePool(goblinPool, goblinPrefab, goblinPoolSize);
        CreatePool(archerPool, archerPrefab, archerPoolSize);
    }

    public void CreatePool(List<GameObject> pool, GameObject prefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentTransform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        List<GameObject> pool = GetPoolForType(type);

        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        return null;
    }

    private List<GameObject> GetPoolForType(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Skeleton:
                return skeletonPool;
            case EnemyType.Goblin:
                return goblinPool;
            case EnemyType.Archer:
                return archerPool;
            default:
                return null;
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(parentTransform);
    }
}

public enum EnemyType
{
    Skeleton,
    Goblin,
    Archer
}
