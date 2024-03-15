using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float baseDamage = 40f; 
    private float currentDamage; 

    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);

        currentDamage = baseDamage;
    }

    public void PlayerDamageUpgrade()
    {
        currentDamage += 2f;
    }

    public void PlayerDamageUpgrade2()
    {
        currentDamage += 5f;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Skeleton skeletonEnemy = collision.GetComponent<Skeleton>();
            if (skeletonEnemy != null)
            {
                skeletonEnemy.EnemyTakeDamage(currentDamage);
            }

            Goblin goblinEnemy = collision.GetComponent<Goblin>();
            if (goblinEnemy != null)
            {
                goblinEnemy.EnemyTakeDamage(currentDamage);
            }

            Archer archerEnemy = collision.GetComponent<Archer>();
            if (archerEnemy != null)
            {
                archerEnemy.EnemyTakeDamage(currentDamage);
            }

            BoDLifeController BoDEnemy = collision.GetComponent<BoDLifeController>();
            if (BoDEnemy != null)
            {
                BoDEnemy.EnemyTakeDamage(currentDamage);
            }

            anim.SetTrigger("FireBallAttack");

            // Spawn hi?u ?ng n? 
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
