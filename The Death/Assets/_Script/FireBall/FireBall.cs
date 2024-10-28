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

    [Header("Sound Settings")]
    public AudioClip fireBallSoundEffect;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        Invoke("ReturnToPool", lifeTime);
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
                float damage = playerPower.playerCurrentDamage;

                if (IsCriticalHit())
                {
                    damage *= 2;
                }

                enemyTakeDamage.TakePlayerDamage(damage);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
/*            SoundFxManager.instance.PlaySoundFXClip(fireBallSoundEffect, transform, 1f);
*/
            /*            ReturnToPool();*/
        }
    }

    private void ReturnToPool()
    {
        CancelInvoke();
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
