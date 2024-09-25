using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float speed = 50f;

    private Rigidbody2D rb;
    private GameObject currentTarget;  // M?c ti�u hi?n t?i
    private int hitCount = 0;  // ??m s? l?n va ch?m
    private int maxHits = 5;   // T?i ?a 5 l?n

    public PlayerPower playerPower;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        Destroy(gameObject, 3f);  // Hu? sau th?i gian lifeTime

        // T�m m?c ti�u k? ??ch ng?u nhi�n ??u ti�n
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
            // Di chuy?n v? ph�a k? ??ch hi?n t?i
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // ?i?u ch?nh g�c ?? c?a tia s�t theo h??ng di chuy?n
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
                    // T�m k? ??ch ng?u nhi�n v� ti?p t?c bay ??n
                    currentTarget = FindRandomEnemy();
                    if (currentTarget != null)
                    {
                        MoveTowardsTarget();
                    }
                    else
                    {
                        // Kh�ng t�m th?y k? ??ch ti?p theo, ph� h?y tia s�t
                        Destroy(gameObject);
                    }
                }
                else
                {
                    // ?� ??t s? l?n va ch?m t?i ?a
                    Destroy(gameObject);
                }
            }
        }
    }

    // T�m k? ??ch ng?u nhi�n t? danh s�ch k? ??ch
    private GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            return null;  // N?u kh�ng c�n k? ??ch
        }

        // Ch?n ng?u nhi�n 1 k? ??ch t? danh s�ch
        int randomIndex = Random.Range(0, enemies.Length);
        return enemies[randomIndex];
    }

    // Di chuy?n v? ph�a m?c ti�u
    private void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // ?i?u ch?nh g�c ?? c?a tia s�t khi di chuy?n v? ph�a m?c ti�u
            RotateTowardsTarget(direction);
        }
    }

    // Xoay tia s�t v? h??ng m?c ti�u
    private void RotateTowardsTarget(Vector2 direction)
    {
        // T�nh to�n g�c gi?a h??ng hi?n t?i v� m?c ti�u
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // ??t g�c quay c?a tia s�t
    }
}
