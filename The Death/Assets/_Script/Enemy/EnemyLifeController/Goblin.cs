using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : EnemyLifeBase
{
    [SerializeField] protected float goblinMaxHealth = 800f;
    [SerializeField] protected float goblinHealth;
    public float goblinDamage = 5f;

    public GameObject attackArea;
    public float attackRadius = 7.59f;
    public float attackAngle = 98f;
    public float attackDelay = 0.5f;
    public float cooldownTime = 1f;
    public int damage = 10;
    private Transform player;
    private bool isAttacking = false;
    private bool hasStartedAttackSequence = false;
    private bool isCooldown = false;

    [Header("Sound Settings")]
    public AudioClip goblinDeathSoundEffect;


    public override void Start()
    {
        goblinHealth = goblinMaxHealth;
        enemyMaxHealth = goblinMaxHealth;
        enemyHealth = goblinHealth;
        enemyDamage = goblinDamage;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        attackArea.SetActive(false);
        base.Start();
    }



    void Update()
    {
        if (isCooldown) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius && !hasStartedAttackSequence)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

            if (angleToPlayer <= attackAngle / 2)
            {
                if (!isAttacking)
                {
                    attackArea.SetActive(true);
                    isAttacking = true;
                    hasStartedAttackSequence = true;
                    Invoke("StartAttack", attackDelay);
                }
            }
        }
    }

    private void StartAttack()
    {
        anim.SetTrigger("goblinAttack");
        Invoke("AttackPlayer", 0.5f);
        StartCoroutine(Cooldown());
    }

    private void AttackPlayer()
    {
        // Ki?m tra l?i kho?ng cách và góc gi?a quái v?t và ng??i ch?i
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        // Ch? gây sát th??ng n?u ng??i ch?i v?n còn trong ph?m vi và góc t?n công
        if (distanceToPlayer <= attackRadius && angleToPlayer <= attackAngle / 2)
        {
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);
            }
        }

        // T?t vùng t?n công và thi?t l?p l?i tr?ng thái
        attackArea.SetActive(false);
        isAttacking = false;
        hasStartedAttackSequence = false;

        anim.SetTrigger("enemyRun");
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 forward = transform.right;
        Vector3 startPoint = Quaternion.Euler(0, 0, -attackAngle / 2) * forward * attackRadius;

        Vector3 lastPoint = startPoint;
        for (int i = 1; i <= 30; i++)
        {
            float angle = -attackAngle / 2 + (attackAngle / 30) * i;
            Vector3 nextPoint = Quaternion.Euler(0, 0, angle) * forward * attackRadius;
            Gizmos.DrawLine(transform.position + lastPoint, transform.position + nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.DrawLine(transform.position + lastPoint, transform.position);
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        SoundFxManager.instance.PlaySoundFXClip(goblinDeathSoundEffect, transform, 0.4f);

        anim.SetBool("goblinDeath", true);

    }
}
