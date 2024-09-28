using System;
using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("Lightning")]
    [SerializeField] private GameObject lightningPrefab;  
    [SerializeField] private GameObject firingPoint; 

    private PlayerPower playerPower;

    void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        // goi sau 1s va goi lai sau 4s
        InvokeRepeating("AutoAttackNearestEnemy", 1f, 4f);
    }

    private void AutoAttackNearestEnemy()
    {
        // Lay tat ca ke dich co tag enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Tìm ke dich gan nhat
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            // tinh toan huong va goc quay
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            int projectileCount = playerPower.playerCurrentProjectiles; 
            float angleOffset = 15f; 

            if (projectileCount == 1)
            {
                GameObject spawnedBullet = Instantiate(lightningPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, angle));
                spawnedBullet.transform.right = direction; 
            }
            else
            {
                int halfProjectiles = projectileCount / 2;

                for (int i = -halfProjectiles; i <= halfProjectiles; i++)
                {
                    if (projectileCount % 2 == 0 && i == 0)
                        continue;

                    float currentAngle = angle + i * angleOffset;
                    Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                    GameObject spawnedBullet = Instantiate(lightningPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, currentAngle));
                    spawnedBullet.transform.right = bulletDirection; 
                }
            }
        }
    }
}
