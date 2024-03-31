using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float archerMaxHealth = 200f;
    [SerializeField] protected float archerHealth;
    public float archerDamage = 5f;

    public PlayerLife playerLife;
    public HealthBar archerHealthBar;
    private bool isHealthBarVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        archerHealth = archerMaxHealth;
        archerHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        archerHealth -= damage;

        if (!isHealthBarVisible)
        {
            archerHealthBar.SetMaxHealth(archerMaxHealth);
            isHealthBarVisible = true;
        }
        archerHealthBar.SetHealth(archerHealth);
        if (archerHealth <= 0)
        {
            ArcherDie();
        }
    }

    public void ArcherDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("ArcherDeath", true);
        Destroy(gameObject, 1f);

        GetComponent<ExperienceSpawner>().InstantiateLoot(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(archerDamage);
        }
    }
}
