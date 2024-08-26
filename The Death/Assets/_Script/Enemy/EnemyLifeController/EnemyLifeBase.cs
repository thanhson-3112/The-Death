using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBase : MonoBehaviour, IDamageAble
{
    public Rigidbody2D rb;
    public Animator anim;

    [SerializeField] protected float enemyMaxHealth;
    [SerializeField] protected float enemyHealth;
    public float enemyDamage = 5f;

    public PlayerLife playerLife;
    public HealthBar enemyHealthBar;
    private bool isHealthBarVisible = false;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        enemyHealth = enemyMaxHealth;
        enemyHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void TakePlayerDamage(float damage)
    {
        if(enemyHealth > 0)
        {
            enemyHealth -= damage;
        }

        if (!isHealthBarVisible)
        {
            enemyHealthBar.SetMaxHealth(enemyMaxHealth);
            isHealthBarVisible = true;
        }

        enemyHealthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    public virtual void EnemyDie()
    {
        rb.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;

        GetComponent<ExperienceSpawner>().InstantiateLoot(transform.position);
        GetComponent<GoldSpawner>().InstantiateLoot(transform.position);

        // Return the enemy to the pool after a delay
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before returning to the pool
        EnemyPool.Instance.ReturnEnemy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(enemyDamage);
        }
    }

    public void ResetHealth()
    {
        enemyHealth = enemyMaxHealth;
        if (isHealthBarVisible)
        {
            enemyHealthBar.SetHealth(enemyHealth);
        }
        rb.GetComponent<Collider2D>().enabled = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
