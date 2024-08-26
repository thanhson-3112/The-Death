using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GameObject bulletPrefab;
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
            GameObject bullet = Instantiate(bulletPrefab, parentTransform);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.SetParent(parentTransform); // ??t viên ??n làm con c?a object cha
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab, parentTransform);
        newBullet.SetActive(true);
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(parentTransform); // ??m b?o viên ??n v?n là con c?a object cha
    }
}