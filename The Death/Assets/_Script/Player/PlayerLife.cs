using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float health;

    public HealthBar healthBar;

    private PlayerPower playerPower;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();


        health = playerPower.playerCurrentMaxHealth;
        healthBar.SetMaxHealth(playerPower.playerCurrentMaxHealth);
    }

    void Update()
    {
        if(health < playerPower.playerCurrentMaxHealth)
        {
            health += playerPower.playerCurrentHealthRegen;
        }
    }


    public void TakeDamage(float enemyDamage)
    {
        // Tinh sat thuong sau khi tru giap
        float actualDamage = CalculateDamageAfterArmor(enemyDamage, playerPower.playerCurrentArmor);

        health -= actualDamage;
        healthBar.SetHealth(health);

        anim.SetTrigger("PlayerTakeDamage");

        if (health <= 0)
        {
            Die();
        }
    }

    private float CalculateDamageAfterArmor(float damage, float armor)
    {
        // Giap player
        float damageReduction = armor / (armor + 100); 
        float actualDamage = damage * (1 - damageReduction);

        return Mathf.Max(actualDamage, 0); 
    }

    private void Die()
    {
        /*if (DeathSoundEffect != null)
        {
            DeathSoundEffect.Play();
        }*/

        SceneManager.LoadScene(3);
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
        SceneManager.LoadScene(3);
    }
}
