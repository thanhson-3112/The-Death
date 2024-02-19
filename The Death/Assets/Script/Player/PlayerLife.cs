using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public int maxHealth = 5;
    [SerializeField] public int health;

    public HealthBar healthBar;

    /*[SerializeField] public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;*/
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        /*respawnPoint = startPoint.transform.position;*/
    }

    void Update()
    {
      
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        anim.SetTrigger("PlayerTakeDamge");
        /*DamageSoundEffect.Play();*/
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        /*if (DeathSoundEffect != null)
        {
            DeathSoundEffect.Play();
        }*/

        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("PlayerDeath");

        Destroy(gameObject, 1.5f);
        /*Invoke("Respawn", 1.7f);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Blood"))
        {
            if (health != maxHealth)
            {
                *//*HeathSoundEffect.Play();*//*
                Destroy(collision.gameObject);
                health++;
            }
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            *//*HeathSoundEffect.Play();*//*
            Destroy(collision.gameObject);
            maxHealth++;
            health = maxHealth;
        }*/
    }


    private void RestartLevel()
    {
 
    }
}
