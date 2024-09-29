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

        // G?i sau 1s v� g?i l?i sau 4s
        InvokeRepeating("AutoAttackNearestEnemy", 1f, 4f);
    }

    private void AutoAttackNearestEnemy()
    {
        // L?y t?t c? k? ??ch c� tag "Enemy"
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

        if (nearestEnemy != null)
        {
            // T�nh to�n h??ng v� g�c quay
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // B?t ??u b?n 5 vi�n ??n li�n ti?p
            StartCoroutine(ShootBullets(direction, angle));
        }
    }

    private IEnumerator ShootBullets(Vector2 direction, float angle)
    {
        for (int i = 0; i < 5; i++)
        {
            // T?o vi�n ??n v� b?n n� theo h??ng ?� t�nh
            GameObject spawnedBullet = Instantiate(fireGunPrefab, firingPoint.transform.position, Quaternion.Euler(0, 0, angle));
            spawnedBullet.transform.right = direction;

            // Ch? 0.2 gi�y tr??c khi b?n vi�n ??n ti?p theo
            yield return new WaitForSeconds(0.1f);
        }
    }
}
