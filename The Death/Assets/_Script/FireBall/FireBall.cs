using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;

    public PlayerPower playerPower;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageAble enemyTakeDamage = collision.GetComponent<IDamageAble>();
            if (enemyTakeDamage != null)
            {
                enemyTakeDamage.TakePlayerDamage(playerPower.playerCurrentDamage);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
