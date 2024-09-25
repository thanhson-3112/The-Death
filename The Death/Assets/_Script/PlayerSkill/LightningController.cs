using System;
using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;  // Prefab ??n
    [SerializeField] private GameObject firingPoint; // V? trí b?n

    private PlayerPower playerPower;

    // Start is called before the first frame update
    void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        // G?i hàm AutoAttackNearestEnemy sau 1 giây và l?p l?i sau m?i 4 giây
        InvokeRepeating("AutoAttackNearestEnemy", 1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // Các logic c?p nh?t khác (n?u c?n)
    }

    private void AutoAttackNearestEnemy()
    {
        // L?y t?t c? các ??i t??ng có tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Tìm k? ??ch g?n nh?t
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // N?u có k? ??ch g?n nh?t
        if (nearestEnemy != null)
        {
            // Tính toán h??ng và góc quay cho ??n
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            int projectileCount = playerPower.playerCurrentProjectiles; // S? l??ng ??n
            float angleOffset = 15f; // ?? l?ch góc gi?a các viên ??n

            // N?u ch? có 1 viên ??n
            if (projectileCount == 1)
            {
                GameObject spawnedBullet = Instantiate(firePrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, angle));
                spawnedBullet.transform.right = direction; // ??t h??ng cho ??n
            }
            else
            {
                int halfProjectiles = projectileCount / 2;

                // T?o nhi?u ??n theo hình nón
                for (int i = -halfProjectiles; i <= halfProjectiles; i++)
                {
                    // B? qua viên ??n gi?a n?u s? ??n là ch?n
                    if (projectileCount % 2 == 0 && i == 0)
                        continue;

                    // Tính góc hi?n t?i c?a viên ??n
                    float currentAngle = angle + i * angleOffset;
                    Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                    // T?o ??n m?i v?i góc và v? trí t??ng ?ng
                    GameObject spawnedBullet = Instantiate(firePrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, currentAngle));
                    spawnedBullet.transform.right = bulletDirection; // ??t h??ng cho ??n
                }
            }
        }
    }
}
