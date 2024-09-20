using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLifeBase : MonoBehaviour, IDamageAble
{
    private Rigidbody2D rb;
    public Animator anim;

    [SerializeField] protected float enemyMaxHealth;
    [SerializeField] protected float enemyHealth;

    public PlayerLife playerLife;
    public HealthBar enemyHealthBar;
    private bool isHealthBarVisible = false;

    // gay damage cho nguoi choi
    public float enemyDamage = 5f;
    public float damageInterval = 2f;
    private bool isDamaging = false;
    private List<Collision2D> playerInRange = new List<Collision2D>();

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

        // Xoa ke dich 
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(1f); 
        EnemyPool.Instance.ReturnEnemy(gameObject);
    }

    public void ResetHealth()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        enemyHealth = enemyMaxHealth;
        isHealthBarVisible = false; // Reset tr?ng thái thanh máu hi?n th?
        if (enemyHealthBar != null)
        {
            enemyHealthBar.SetHealthBar();
        }
        rb.GetComponent<Collider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!playerInRange.Contains(collision))
            {
                playerInRange.Add(collision);
            }

            if (!isDamaging)
            {
                StartDamage();
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerInRange.Contains(collision))
            {
                playerInRange.Remove(collision);
            }

            if (playerInRange.Count == 0)
            {
                StopDamage();
            }
        }
    }

    // Gay damage cho enemy
    private void StartDamage()
    {
        if (!isDamaging)
        {
            isDamaging = true;
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        while (isDamaging)
        {
            List<Collision2D> enemiesToDamage = new List<Collision2D>(playerInRange);

            foreach (Collision2D enemyCollider in enemiesToDamage)
            {
                playerLife.TakeDamage(1f);
                yield return new WaitForSeconds(0);
            }
            

            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void StopDamage()
    {
        isDamaging = false;
    }
}
