using System;
using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;  // Prefab ??n
    [SerializeField] private GameObject firingPoint; // V? tr� b?n

    private PlayerPower playerPower;

    // Start is called before the first frame update
    void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        // G?i h�m AutoAttackNearestEnemy sau 1 gi�y v� l?p l?i sau m?i 4 gi�y
        InvokeRepeating("AutoAttackNearestEnemy", 1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // C�c logic c?p nh?t kh�c (n?u c?n)
    }

    private void AutoAttackNearestEnemy()
    {
        // L?y t?t c? c�c ??i t??ng c� tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // T�m k? ??ch g?n nh?t
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // N?u c� k? ??ch g?n nh?t
        if (nearestEnemy != null)
        {
            // T�nh to�n h??ng v� g�c quay cho ??n
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            int projectileCount = playerPower.playerCurrentProjectiles; // S? l??ng ??n
            float angleOffset = 15f; // ?? l?ch g�c gi?a c�c vi�n ??n

            // N?u ch? c� 1 vi�n ??n
            if (projectileCount == 1)
            {
                GameObject spawnedBullet = Instantiate(firePrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, angle));
                spawnedBullet.transform.right = direction; // ??t h??ng cho ??n
            }
            else
            {
                int halfProjectiles = projectileCount / 2;

                // T?o nhi?u ??n theo h�nh n�n
                for (int i = -halfProjectiles; i <= halfProjectiles; i++)
                {
                    // B? qua vi�n ??n gi?a n?u s? ??n l� ch?n
                    if (projectileCount % 2 == 0 && i == 0)
                        continue;

                    // T�nh g�c hi?n t?i c?a vi�n ??n
                    float currentAngle = angle + i * angleOffset;
                    Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                    // T?o ??n m?i v?i g�c v� v? tr� t??ng ?ng
                    GameObject spawnedBullet = Instantiate(firePrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, currentAngle));
                    spawnedBullet.transform.right = bulletDirection; // ??t h??ng cho ??n
                }
            }
        }
    }
}
