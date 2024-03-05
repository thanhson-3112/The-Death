using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private float damage;
    private Rigidbody2D rb;

    public GameObject explosionPrefab; 

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            damage = Random.Range(30f, 50f);
            Skeleton skeletonEnemy = collision.GetComponent<Skeleton>();
            if (skeletonEnemy != null)
            {
                skeletonEnemy.EnemyTakeDamage(damage);
            }

            Goblin goblinEnemy = collision.GetComponent<Goblin>();
            if (goblinEnemy != null)
            {
                goblinEnemy.EnemyTakeDamage(damage);
            }
            anim.SetTrigger("FireBallAttack");

            // Hieu ung no
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        }
    }
}
