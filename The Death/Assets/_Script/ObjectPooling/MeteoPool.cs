using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoPool : MonoBehaviour
{
    public static MeteoPool Instance;

    [SerializeField] private GameObject meteoPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> pool;

    // Object cha ch?a các viên ??n
    [SerializeField] private Transform parentTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject meteo = Instantiate(meteoPrefab, parentTransform);
            meteo.SetActive(false);
            pool.Add(meteo);
        }
    }

    public GameObject GetMeteo()
    {
        foreach (var meteo in pool)
        {
            if (!meteo.activeInHierarchy)
            {
                meteo.SetActive(true);
                meteo.transform.SetParent(parentTransform); // ??t viên ??n làm con c?a object cha
                return meteo;
            }
        }

        GameObject newMeteo = Instantiate(meteoPrefab, parentTransform);
        newMeteo.SetActive(true);
        pool.Add(newMeteo);
        return newMeteo;
    }

    public void ReturnMeteo(GameObject meteo)
    {
        meteo.SetActive(false);
        meteo.transform.SetParent(parentTransform); // ??m b?o viên ??n v?n là con c?a object cha
    }
}
