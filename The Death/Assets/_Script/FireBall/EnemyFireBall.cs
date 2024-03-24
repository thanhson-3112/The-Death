using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private float archerDamage = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLife playerLife = collision.GetComponent<PlayerLife>();
        if (collision.CompareTag("Player"))
        {
            if (playerLife != null)
            {
                playerLife.TakeDamage(archerDamage);
            }
        }
    }
}
