using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWorm : EnemyLifeBase
{
    [SerializeField] protected float DWMaxHealth = 500f;
    [SerializeField] protected float DWHealth;
    public float flyingDamage = 5f;

    public GameObject attackArea;
    public float attackRadius = 5f;
    public float attackDelay = 0.5f;
    public int damage = 10;
    public float cooldownTime = 2f;
    private Transform player;
    private bool hasDetectedPlayer = false;
    private bool isAttacking = false;
    private bool isOnCooldown = false;

    public override void Start()
    {
        DWHealth = DWMaxHealth;
        enemyMaxHealth = DWMaxHealth;
        enemyHealth = DWHealth;
        enemyDamage = flyingDamage;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        attackArea.SetActive(false);

        base.Start();
    }

    private void Update()
    {
        if (!hasDetectedPlayer)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRadius && !isOnCooldown)
            {
                hasDetectedPlayer = true;
                attackArea.SetActive(true);

                if (!isAttacking)
                {
                    isAttacking = true;
                    Invoke("StartAttack", attackDelay);
                }
            }
        }
    }

    private void StartAttack()
    {

        anim.SetTrigger("DWAttack");
        Invoke("AttackPlayer", 0.5f);
    }

    private void AttackPlayer()
    {
        // Ki?m tra l?i kho?ng cách và góc gi?a quái v?t và ng??i ch?i
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        // Ch? gây sát th??ng n?u ng??i ch?i v?n còn trong ph?m vi và góc t?n công
        if (distanceToPlayer <= Mathf.Abs(attackRadius) && angleToPlayer <= attackRadius / 2)
        {
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);
            }
        }

        isAttacking = false;
        attackArea.SetActive(false);

        StartCoroutine(StartCooldown());
    }

    // quan ly hoi chieu
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        anim.SetTrigger("DWWalk");
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
        hasDetectedPlayer = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        anim.SetBool("DWDeath", true);
    }
}
