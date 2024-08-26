using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 mousePos;
    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private Transform firing;

    private float fireTimer;

    private PlayerMovement playerMovement;
    [SerializeField] private float slowdownAmount = 10f;

    private bool autoAttacking = false; // Kiểm soát tấn công tự động

    private PlayerPower playerPower;

    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();


        Shoot();
    }

    public void UpgradeAttackSpeed()
    {
        playerPower.playerCurrentAbilityHaste = playerPower.playerCurrentAbilityHaste - 0.1f;
        if (playerPower.playerCurrentAbilityHaste < 0.3)
        {
            playerPower.playerCurrentAbilityHaste = 0.3f;
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Chuyển đổi trạng thái tấn công tự động
            autoAttacking = !autoAttacking;
        }

        if ((autoAttacking || Input.GetMouseButtonDown(0)) && fireTimer <= 0f)
        {
            anim.SetTrigger("attack");
            fireTimer = playerPower.playerCurrentAbilityHaste;
            playerMovement.SlowDown(slowdownAmount);

            if (!autoAttacking)
            {
                MouseAttack();
            }
            else
            {
                AutoAttackNearestEnemy();
            }

        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (fireTimer <= 0f)
        {
            playerMovement.RestoreSpeed();
        }
    }

    private void AutoAttackNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

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
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            int projectileCount = playerPower.playerCurrentProjectiles;
            float angleOffset = 30f;

            if (projectileCount == 1)
            {
                GameObject spawnedBullet = BulletPool.Instance.GetBullet();
                spawnedBullet.transform.position = firing.position;
                spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
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

                    GameObject spawnedBullet = BulletPool.Instance.GetBullet();
                    spawnedBullet.transform.position = firing.position;
                    spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                    spawnedBullet.transform.right = bulletDirection;
                }
            }
        }
    }

    private void MouseAttack()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        firingPoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        int projectileCount = playerPower.playerCurrentProjectiles;
        float angleOffset = 30f;

        if (projectileCount == 1)
        {
            GameObject spawnedBullet = BulletPool.Instance.GetBullet();
            spawnedBullet.transform.position = firing.position;
            spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
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

                GameObject spawnedBullet = BulletPool.Instance.GetBullet();
                spawnedBullet.transform.position = firing.position;
                spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                spawnedBullet.transform.right = bulletDirection;
            }
        }
    }

}
