using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] public float damage = 20f;
    private Rigidbody2D rb;

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            speed = 9;
            Skeleton enemy = collision.GetComponent<Skeleton>();
            if (enemy != null)
            {
                enemy.SkeletonTakeDamage(damage);
            }
            anim.SetTrigger("FireBallAttack");
            Destroy(gameObject,0.4f);
        }
    }
}
