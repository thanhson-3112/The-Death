using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float flyingMaxHealth = 200f;
    [SerializeField] protected float flyingHealth;
    public float flyingDamage = 5f;

    public PlayerLife playerLife;
    public HealthBar flyingHealthBar;
    private bool isHealthBarVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        flyingHealth = flyingMaxHealth;
        flyingHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        flyingHealth -= damage;

        if (!isHealthBarVisible)
        {
            flyingHealthBar.SetMaxHealth(flyingMaxHealth);
            isHealthBarVisible = true;
        }
        flyingHealthBar.SetHealth(flyingHealth);
        if (flyingHealth <= 0)
        {
            GoblinDie();
        }
    }

    public void GoblinDie()
    {
        rb.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;

        anim.SetBool("FlyingDeath", true);
        Destroy(gameObject, 1f);

        GetComponent<ExperienceSpawner>().InstantiateLoot(transform.position);
        GetComponent<GoldSpawner>().InstantiateLoot(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(flyingDamage);
        }
    }
}
