using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float maxHealth = 5f;
    [SerializeField] public float health;

    public HealthBar healthBar;

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


    public void TakeDamage(float skeletonDamage)
    {
        
        if(skeletonDamage < 2f)
        {
            health -= skeletonDamage;
            Debug.Log("dame be hon 2");
        }
        else
        {
            Debug.Log("dame lon hon 2");

        }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
