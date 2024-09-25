using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float speed = 50f;

    private Rigidbody2D rb;
    private GameObject currentTarget;  // M?c tiêu hi?n t?i
    private int hitCount = 0;  // ??m s? l?n va ch?m
    private int maxHits = 5;   // T?i ?a 5 l?n

    public PlayerPower playerPower;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        Destroy(gameObject, 3f);  // Hu? sau th?i gian lifeTime

        // Tìm m?c tiêu k? ??ch ng?u nhiên ??u tiên
        currentTarget = FindRandomEnemy();
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        if (currentTarget != null)
        {
            // Di chuy?n v? phía k? ??ch hi?n t?i
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // ?i?u ch?nh góc ?? c?a tia sét theo h??ng di chuy?n
            RotateTowardsTarget(direction);
        }

        if(hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }

    public bool IsCriticalHit()
    {
        return Random.Range(0f, 100f) < playerPower.playerCurrentCritChance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject == currentTarget)
        {
            IDamageAble enemyTakeDamage = collision.GetComponent<IDamageAble>();
            if (enemyTakeDamage != null)
            {
                float damage = playerPower.playerCurrentDamage;

                if (IsCriticalHit())
                {
                    damage *= 2;
                }

                enemyTakeDamage.TakePlayerDamage(damage);
                hitCount++;  // ??m l?n va ch?m

                if (hitCount < maxHits)
                {
                    // Tìm k? ??ch ng?u nhiên và ti?p t?c bay ??n
                    currentTarget = FindRandomEnemy();
                    if (currentTarget != null)
                    {
                        MoveTowardsTarget();
                    }
                    else
                    {
                        // Không tìm th?y k? ??ch ti?p theo, phá h?y tia sét
                        Destroy(gameObject);
                    }
                }
                else
                {
                    // ?ã ??t s? l?n va ch?m t?i ?a
                    Destroy(gameObject);
                }
            }
        }
    }

    // Tìm k? ??ch ng?u nhiên t? danh sách k? ??ch
    private GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            return null;  // N?u không còn k? ??ch
        }

        // Ch?n ng?u nhiên 1 k? ??ch t? danh sách
        int randomIndex = Random.Range(0, enemies.Length);
        return enemies[randomIndex];
    }

    // Di chuy?n v? phía m?c tiêu
    private void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // ?i?u ch?nh góc ?? c?a tia sét khi di chuy?n v? phía m?c tiêu
            RotateTowardsTarget(direction);
        }
    }

    // Xoay tia sét v? h??ng m?c tiêu
    private void RotateTowardsTarget(Vector2 direction)
    {
        // Tính toán góc gi?a h??ng hi?n t?i và m?c tiêu
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // ??t góc quay c?a tia sét
    }
}
