using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyMovement
{
    [SerializeField] protected float skeletonMaxHealth = 100f;
    [SerializeField] protected float skeletonHealth;
    public float skeletonDamage = 1f;

    public PlayerLife playerLife;
    public HealthBar skeletonHealthBar;
    private bool isHealthBarVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        skeletonHealth = skeletonMaxHealth;
        skeletonHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        skeletonHealth -= damage;
        
        if (!isHealthBarVisible)
        {
            skeletonHealthBar.SetMaxHealth(skeletonMaxHealth);
            isHealthBarVisible = true;
        }
        skeletonHealthBar.SetHealth(skeletonHealth);
        if (skeletonHealth <= 0)
        {
            SkeletonDie();
        }
    }

    void SkeletonDie()
    {
        _enemySpeed = 0;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("SkeletonDeath", true);
        Destroy(gameObject, 1f);

        GetComponent<ExperienceSpawner>().InstantiateLoot(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(skeletonDamage);
        }
    }
}
