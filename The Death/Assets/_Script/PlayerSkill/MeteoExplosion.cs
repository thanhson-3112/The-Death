using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoExplosion : MonoBehaviour
{
    public float meteoExplosionDamage = 5f;
    public float damageInterval = 1f;

    private bool isDamaging = false;
    private List<Collider2D> enemiesInRange = new List<Collider2D>();

    public PlayerPower playerPower;

    public void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(collision))
            {
                enemiesInRange.Add(collision);
            }

            if (!isDamaging)
            {
                StartDamage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(collision))
            {
                enemiesInRange.Remove(collision);
            }

            if (enemiesInRange.Count == 0)
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
            List<Collider2D> enemiesToDamage = new List<Collider2D>(enemiesInRange);
            foreach (Collider2D enemyCollider in enemiesToDamage)
            {
                IDamageAble enemyTakeDamage = enemyCollider.GetComponent<IDamageAble>();
                if (enemyTakeDamage != null)
                {
                    enemyTakeDamage.TakePlayerDamage(meteoExplosionDamage);
                }

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
