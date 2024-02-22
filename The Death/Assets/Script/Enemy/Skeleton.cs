using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skeleton : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float skeletonMaxHealth = 100f;
    [SerializeField] protected float skeletonHealth;
    public float enemyDamage = 1f;

    public PlayerLife playerLife;
    public HealthBar skeletonHealthBar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        skeletonHealth = skeletonMaxHealth;
        skeletonHealthBar.SetMaxHealth(skeletonMaxHealth);
    }

    void Update()
    {
      
    }

    public virtual void SkeletonTakeDamage(float damage)
    {
        skeletonHealth -= damage;
        skeletonHealthBar.SetHealth(skeletonHealth);
        if (skeletonHealth <= 0)
        {
            SkeletonDie();
        }
    }

    void SkeletonDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("EnemyDeath");
        Destroy(gameObject, 1.25f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(enemyDamage);
        }
    }
}
