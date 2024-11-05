using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyLifeBase
{
    [SerializeField] protected float skeletonMaxHealth = 300f;
    [SerializeField] protected float skeletonHealth;
    public float skeletonDamage = 1f;

    public GameObject attackArea;
    public float attackRadius = 3.11f;
    public float attackAngle = 144f;
    public float attackDelay = 0.5f;
    public float cooldownTime = 1f;
    public int damage = 10;
    private Transform player;
    private bool isAttacking = false;
    private bool hasStartedAttackSequence = false;
    private bool isCooldown = false;

    [Header("Sound Settings")]
    public AudioClip skeletonDeathSoundEffect;

    public override void Start()
    {
        skeletonHealth = skeletonMaxHealth;
        enemyMaxHealth = skeletonMaxHealth;
        enemyHealth = skeletonHealth;
        enemyDamage = skeletonDamage;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackArea.SetActive(false);

        base.Start();
    }

    void Update()
    {
        if (isCooldown) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ki?m tra n?u ng??i ch?i n?m trong ph?m vi t?n công
        if (distanceToPlayer <= Mathf.Abs(attackRadius) && !hasStartedAttackSequence)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

            // Xác ??nh ng??i ch?i ?ang ? bên trái hay bên ph?i c?a quái v?t
            if (player.position.x > transform.position.x)
            {
                // Ng??i ch?i bên ph?i - quay `attackArea` theo h??ng ph?i
                attackArea.transform.localRotation = Quaternion.Euler(0, 0, 0);
                attackRadius = Mathf.Abs(attackRadius);
            }
            else
            {
                // Ng??i ch?i bên trái - quay `attackArea` theo h??ng trái
                attackRadius = -Mathf.Abs(attackRadius);
                attackArea.transform.localRotation = Quaternion.Euler(0, 180, 0);

            }

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
        anim.SetTrigger("SkeletonAttack");
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
        if (distanceToPlayer <= Mathf.Abs(attackRadius) && angleToPlayer <= attackAngle / 2)
        {
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);
            }
        }

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

    public override void EnemyDie()
    {
        CancelInvoke("AttackPlayer");
        base.EnemyDie();
        anim.SetBool("SkeletonDeath", true);
        SoundFxManager.instance.PlaySoundFXClip(skeletonDeathSoundEffect, transform, 0.4f);

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
}
