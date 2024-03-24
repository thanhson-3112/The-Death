using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float goblinMaxHealth = 200f;
    [SerializeField] protected float goblinHealth;
    public float goblinDamage = 5f;

    public PlayerLife playerLife;
    public HealthBar goblinHealthBar;
    private bool isHealthBarVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        goblinHealth = goblinMaxHealth;
        goblinHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        goblinHealth -= damage;

        if (!isHealthBarVisible)
        {
            goblinHealthBar.SetMaxHealth(goblinMaxHealth);
            isHealthBarVisible = true;
        }
        goblinHealthBar.SetHealth(goblinHealth);
        if (goblinHealth <= 0)
        {
            GoblinDie();
        }
    }

    public void GoblinDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("goblinDeath", true);
        Destroy(gameObject, 1f);

        GetComponent<LootSpawner>().InstantiateLoot(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(goblinDamage);
        }
    }
}
