using System.Collections.Generic;
using UnityEngine;

public class MapPoolManager : MonoBehaviour
{
    public static MapPoolManager Instance;

    [SerializeField] private GameObject mapPrefab; // Prefab c?a b?n ??
    [SerializeField] private int poolSize = 2; // S? l??ng b?n ?? trong pool
    private Queue<GameObject> mapPool;

    private void Awake()
    {
        Instance = this;
        mapPool = new Queue<GameObject>();

        // T?o tr??c các b?n ?? và add vào hàng ??i
        for (int i = 0; i < poolSize; i++)
        {
            GameObject map = Instantiate(mapPrefab);
            map.SetActive(false);
            mapPool.Enqueue(map);
        }
    }

    // L?y b?n ?? t? pool
    public GameObject GetMap(Vector3 position)
    {
        if (mapPool.Count > 0)
        {
            GameObject map = mapPool.Dequeue();
            map.transform.position = position;
            map.SetActive(true);
            return map;
        }
        else
        {
            // N?u pool tr?ng, t?o m?i
            GameObject newMap = Instantiate(mapPrefab);
            newMap.transform.position = position;
            return newMap;
        }
    }

    // Tr? b?n ?? v? pool
    public void ReturnMap(GameObject map)
    {
        map.SetActive(false);
        mapPool.Enqueue(map);
    }
}
