using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoDLifeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float BoDMaxHealth = 1000f;
    [SerializeField] protected float BoDHealth;
    public float BoDDamage = 5f;

    public PlayerLife playerLife;
    public HealthBar BoDHealthBar;
    private bool isHealthBarVisible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        BoDHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<HealthBar>();

        BoDHealth = BoDMaxHealth;
        BoDHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        BoDHealth -= damage;
        anim.SetTrigger("BoDTakeHit");
        anim.SetTrigger("BoDRun");
        if (!isHealthBarVisible)
        {
            BoDHealthBar.SetMaxHealth(BoDMaxHealth);
            isHealthBarVisible = true;
        }

        BoDHealthBar.SetHealth(BoDHealth);
        if (BoDHealth <= 0)
        {
            BoDDie();
        }
    }

    void BoDDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("BoDDeath", true);
        Destroy(gameObject, 1f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(BoDDamage);
        }
    }
}
