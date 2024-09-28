using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 1.5f;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private Animator anim;

    private GameObject targetEnemy;
    private Vector2 targetPosition;

    public GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("ReturnToMeteo", lifeTime);
        FindTargetEnemy();

        // Ki?m tra n?u không có m?c tiêu, phá h?y thiên th?ch sau m?t th?i gian
        if (targetEnemy == null)
        {
            DestroyMeteo();
        }
    }

    private void FindTargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector2 playerPosition = player.transform.position; // L?y v? trí ng??i ch?i

        foreach (GameObject enemy in enemies)
        {
            float distanceToPlayer = Vector2.Distance(playerPosition, enemy.transform.position);

            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            targetPosition = closestEnemy.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Flip theo h??ng enemy
            if (moveDirection.x < 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.velocity = moveDirection * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageAble enemyTakeDamage = collision.GetComponent<IDamageAble>();
            if (enemyTakeDamage != null)
            {
                enemyTakeDamage.TakePlayerDamage(PlayerPower.instance.CurrentMeteoDamage);
            }

            ReturnToMeteo();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    public void ReturnToMeteo()
    {
        MeteoPool.Instance.ReturnMeteo(gameObject);
    }

    private void DestroyMeteo()
    {
        // T?o hi?u ?ng n? tr??c khi phá h?y
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Tr? v? pool ho?c phá h?y n?u không có pool
        MeteoPool.Instance.ReturnMeteo(gameObject); // Ho?c s? d?ng Destroy(gameObject) n?u không có pool
    }
}
