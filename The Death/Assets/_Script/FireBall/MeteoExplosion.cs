using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoExplosion : MonoBehaviour
{
    public float meteoExplosionDamage = 5f;
    public float damageInterval = 1f;

    private bool isDamaging = false;
    private List<Collider2D> enemiesInRange = new List<Collider2D>();

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
                Skeleton skeletonEnemy = enemyCollider.GetComponent<Skeleton>();
                if (skeletonEnemy != null)
                {
                    skeletonEnemy.EnemyTakeDamage(meteoExplosionDamage);
                }

                Goblin goblinEnemy = enemyCollider.GetComponent<Goblin>();
                if (goblinEnemy != null)
                {
                    goblinEnemy.EnemyTakeDamage(meteoExplosionDamage);
                }

                Archer archerEnemy = enemyCollider.GetComponent<Archer>();
                if (archerEnemy != null)
                {
                    archerEnemy.EnemyTakeDamage(meteoExplosionDamage);
                }

                Flying flyingEnemy = enemyCollider.GetComponent<Flying>();
                if (flyingEnemy != null)
                {
                    flyingEnemy.EnemyTakeDamage(meteoExplosionDamage);
                }

                BoDLifeController BoDEnemy = enemyCollider.GetComponent<BoDLifeController>();
                if (BoDEnemy != null)
                {
                    BoDEnemy.EnemyTakeDamage(meteoExplosionDamage);
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
