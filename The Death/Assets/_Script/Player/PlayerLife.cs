using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float health;

    public HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI healthRegenText;


    private PlayerPower playerPower;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        health = playerPower.playerCurrentMaxHealth;
        healthBar.SetMaxHealth(playerPower.playerCurrentMaxHealth);
        healthBar.SetHealth(health);

        // Start the health regeneration coroutine
        StartCoroutine(HealthRegen());
    }

    void Update()
    {
        healthBar.SetMaxHealth(playerPower.playerCurrentMaxHealth);
        healthBar.SetHealth(health);

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        int currentHealth = Mathf.FloorToInt(health); // Convert health to an integer
        int maxHealth = Mathf.FloorToInt(playerPower.playerCurrentMaxHealth); // Convert max health to an integer

        healthText.text = $"{currentHealth} / {maxHealth}";
        healthRegenText.text = $"+ {playerPower.playerCurrentHealthRegen}";
    }

    private IEnumerator HealthRegen()
    {
        while (true)
        {
            if (health < playerPower.playerCurrentMaxHealth)
            {
                health += playerPower.playerCurrentHealthRegen;
                health = Mathf.Min(health, playerPower.playerCurrentMaxHealth); // Cap health to max health
            }
            yield return new WaitForSeconds(1f); // Regenerate health every 1 second
        }
    }

    public void TakeDamage(float enemyDamage)
    {
        // Calculate damage after armor reduction
        float actualDamage = CalculateDamageAfterArmor(enemyDamage, playerPower.playerCurrentArmor);

        if(health >= 0)
        {
            health -= actualDamage;
        }

        anim.SetTrigger("PlayerTakeDamage");

        if (health <= 0)
        {
            Die();
        }
    }

    private float CalculateDamageAfterArmor(float damage, float armor)
    {
        // Armor damage reduction formula
        float damageReduction = armor / (armor + 100);
        float actualDamage = damage * (1 - damageReduction);

        return Mathf.Max(actualDamage, 0);
    }

    private void Die()
    {
        StartCoroutine(WaitAndLoadScene(2.0f));
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("PlayerDeath");

        Destroy(gameObject, 1.5f);
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(3);
    }

    public void Heal()
    {
        health += 10;
        health = Mathf.Min(health, playerPower.playerCurrentMaxHealth); // Cap health to max health
    }
}
