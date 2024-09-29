using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunController : MonoBehaviour
{
    [Header("Lightning")]
    [SerializeField] private GameObject fireGunPrefab;
    [SerializeField] private GameObject firingPoint;

    private PlayerPower playerPower;

    void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        // G?i sau 1s và g?i l?i sau 4s
        InvokeRepeating("AutoAttackNearestEnemy", 1f, 4f);
    }

    private void AutoAttackNearestEnemy()
    {
        // L?y t?t c? k? ??ch có tag "Enemy"
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

        if (nearestEnemy != null)
        {
            // Tính toán h??ng và góc quay
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // B?t ??u b?n 5 viên ??n liên ti?p
            StartCoroutine(ShootBullets(direction, angle));
        }
    }

    private IEnumerator ShootBullets(Vector2 direction, float angle)
    {
        for (int i = 0; i < 5; i++)
        {
            // T?o viên ??n và b?n nó theo h??ng ?ã tính
            GameObject spawnedBullet = Instantiate(fireGunPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, angle));
            spawnedBullet.transform.right = direction;

            // Ch? 0.2 giây tr??c khi b?n viên ??n ti?p theo
            yield return new WaitForSeconds(0.1f);
        }
    }
}
