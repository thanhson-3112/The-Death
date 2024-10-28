using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : EnemyLifeBase
{
    [SerializeField] protected float flyingMaxHealth = 1000f;
    [SerializeField] protected float flyingHealth;
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
        flyingHealth = flyingMaxHealth;
        enemyMaxHealth = flyingMaxHealth;
        enemyHealth = flyingHealth;
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

        anim.SetTrigger("FlyingAttack");
        Invoke("AttackPlayer", 0.5f);  
    }

    private void AttackPlayer()
    {
        if (playerLife != null)
        {
            playerLife.TakeDamage(1f);  
        }

        isAttacking = false;
        attackArea.SetActive(false);  

        StartCoroutine(StartCooldown());
    }

    // quan ly hoi chieu
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true; 
        anim.SetTrigger("FlyingRun");
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
        anim.SetBool("FlyingDeath", true);
    }
}
