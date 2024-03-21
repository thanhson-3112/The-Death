using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoDLifeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float BoDMaxHealth = 1000f;
    [SerializeField] protected float BoDHealth;
    public float BoDDamage = 5f;

    public PlayerLife playerLife;
    public BossHealthBar BoDHealthBar;
    private bool isHealthBarVisible = false;

    public List<GameObject> weapons; // Danh sách các v? khí
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        BoDHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<BossHealthBar>();

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

        for (int i = 0; i < 3; i++)
        {
            if (weapons.Count > 0)
            {
                int randomIndex = Random.Range(0, weapons.Count);
                GameObject weaponToSpawn = weapons[randomIndex];

                // T?o m?t v? trí spawn ng?u nhiên trong bán kính 1 ??n v? t? v? trí c?a boss
                Vector2 randomSpawnOffset = Random.insideUnitCircle.normalized * 3;
                Vector3 spawnPosition = transform.position + (Vector3)randomSpawnOffset;

                Instantiate(weaponToSpawn, spawnPosition, Quaternion.identity);
            }
        }


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
