using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float health;

    public HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI healthRegenText;

    private PlayerPower playerPower;

    [Header("Sound Settings")]
    public AudioClip playerDeathSoundEffect;
    public AudioClip playerHealthSoundEffect;
    public AudioClip playerTakeDamageSoundEffect;




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
                health = Mathf.Min(health, playerPower.playerCurrentMaxHealth); 
            }
            yield return new WaitForSeconds(1f); 
        }
    }

    public void TakeDamage(float enemyDamage)
    {
        // Tinh toan luong damage thuc nhan
        float actualDamage = CalculateDamageAfterArmor(enemyDamage, playerPower.playerCurrentArmor);

        if(health > 0)
        {
            health -= actualDamage;
            health = Mathf.Max(health, 0);
/*            SoundFxManager.instance.PlaySoundFXClip(playerTakeDamageSoundEffect, transform, 1f);
*/        }

        anim.SetTrigger("PlayerTakeDamage");

        if (health <= 0)
        {
            Die();
        }
    }

    private float CalculateDamageAfterArmor(float damage, float armor)
    {
        // tinh toan luong giap
        float damageReduction = armor / (armor + 100);
        float actualDamage = damage * (1 - damageReduction);

        return Mathf.Max(actualDamage, 0);
    }

    private void Die()
    {
        anim.SetTrigger("PlayerDeath");
        SoundFxManager.instance.PlaySoundFXClip(playerDeathSoundEffect, transform, 1f);
        rb.bodyType = RigidbodyType2D.Static; 
        GetComponent<Collider2D>().enabled = false; 
        StartCoroutine(WaitAndLoadScene(2.0f));
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(4);
    }

    public void Heal()
    {
        health += 10;
        health = Mathf.Min(health, playerPower.playerCurrentMaxHealth);
        SoundFxManager.instance.PlaySoundFXClip(playerHealthSoundEffect, transform, 1f);
    }
}
