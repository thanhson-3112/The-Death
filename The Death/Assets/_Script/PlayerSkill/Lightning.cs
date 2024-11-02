using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float speed = 50f;

    private Rigidbody2D rb;
    private GameObject currentTarget;
    private int hitCount = 0;
    private int maxHits = 5;

    public GameObject explosionPrefab;
    public PlayerPower playerPower;

    [Header("Sound Settings")]
    public AudioClip lightningSoundEffect;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        currentTarget = FindClosestEnemy();
        if (currentTarget != null)
        {
            MoveTowards(currentTarget.transform);
        }

        Destroy(gameObject, 10f); 
    }

    // T�m k? ??ch g?n nh?t
    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // T�m k? ??ch ng?u nhi�n
    private GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        // Ch?n m?t k? ??ch ng?u nhi�n trong danh s�ch
        int randomIndex = Random.Range(0, enemies.Length);
        return enemies[randomIndex];
    }

    // Di chuy?n v? ph�a k? ??ch
    private void MoveTowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // Xoay tia s�t theo g�c ?� t�nh
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
                float damage = playerPower.CurrentLightningDamage;

                if (IsCriticalHit())
                {
                    damage *= 2;
                }

                enemyTakeDamage.TakePlayerDamage(damage);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                SoundFxManager.instance.PlaySoundFXClip(lightningSoundEffect, transform, 0.5f);

            }

            // T?ng s? l??ng l?n ?�nh
            hitCount++;

            if (hitCount >= maxHits)
            {
                Destroy(gameObject); // H?y tia s�t n?u ?� ??t s? l??ng m?c ti�u t?i ?a
            }
            else
            {
                // T�m k? ??ch m?i v� ti?p t?c di chuy?n ??n ?�
                currentTarget = FindRandomEnemy();
                if (currentTarget != null)
                {
                    MoveTowards(currentTarget.transform);
                }
                else
                {
                    Destroy(gameObject); // H?y n?u kh�ng t�m th?y k? ??ch m?i
                }
            }
        }
    }
}
