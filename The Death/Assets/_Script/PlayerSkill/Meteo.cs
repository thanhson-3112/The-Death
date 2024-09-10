using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private Animator anim;

    private GameObject targetEnemy; 
    private Vector2 targetPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);

        FindTargetEnemy();
    }

    private void FindTargetEnemy()
    {
        targetEnemy = GameObject.FindGameObjectWithTag("Enemy");

        if (targetEnemy != null)
        {
            targetPosition = targetEnemy.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Flip theo huong enemy
            if (moveDirection.x < 0f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.velocity = moveDirection * speed;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageAble enemyTakeDamage = collision.GetComponent<IDamageAble>();
            if (enemyTakeDamage != null)
            {
                enemyTakeDamage.TakePlayerDamage(PlayerPower.instance.meteoDamage);
            }

            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
