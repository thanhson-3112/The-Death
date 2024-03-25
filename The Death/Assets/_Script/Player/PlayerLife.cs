using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float maxHealth = 30f;
    [SerializeField] public float health;

    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
      
    }


    public void TakeDamage(float enemyDamage)
    {
        health -= enemyDamage;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void Heal()
    {
        health += 10;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
