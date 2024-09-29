using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;

    public PlayerPower playerPower;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    public bool IsCriticalHit()
    {
        return Random.Range(0f, 100f) < playerPower.playerCurrentCritChance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageAble enemyTakeDamage = collision.GetComponent<IDamageAble>();
            if (enemyTakeDamage != null)
            {
                float damage = playerPower.CurrentFireGunDamage;

                if (IsCriticalHit())
                {
                    damage *= 2;
                }

                enemyTakeDamage.TakePlayerDamage(damage);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
