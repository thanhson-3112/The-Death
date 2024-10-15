using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyLifeBase
{
    [SerializeField] protected float skeletonMaxHealth = 100f;
    [SerializeField] protected float skeletonHealth;
    public float skeletonDamage = 1f;

    public GameObject attackArea;
    public float attackRadius = 3.11f;  // B�n k�nh v�ng t?n c�ng
    public float attackAngle = 144f;    // G�c m? c?a v�ng b�n nguy?t (180 ??)
    public float attackDelay = 0.5f;    // ?? tr? tr??c khi t?n c�ng
    public int damage = 10;             // L??ng s�t th??ng g�y ra khi t?n c�ng
    private Transform player;
    private bool isPlayerInRange = false;
    private bool isAttacking = false;

    public override void Start()
    {
        skeletonHealth = skeletonMaxHealth;
        enemyMaxHealth = skeletonMaxHealth;
        enemyHealth = skeletonHealth;
        enemyDamage = skeletonDamage;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackArea.SetActive(false);  // ?n attack area ban ??u

        base.Start();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // N?u ng??i ch?i ? trong b�n k�nh t?n c�ng
        if (distanceToPlayer <= attackRadius)
        {
            // Ki?m tra g�c gi?a h??ng Skeleton v� v? tr� ng??i ch?i
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer); // H??ng transform.right l� h??ng Skeleton ?ang ??i m?t

            // N?u ng??i ch?i trong v�ng b�n nguy?t (g�c)
            if (angleToPlayer <= attackAngle / 2)
            {
                if (!isAttacking)
                {
                    // Hi?n th? v�ng t?n c�ng
                    attackArea.SetActive(true);

                    // B?t ??u qu� tr�nh t?n c�ng sau th?i gian tr?
                    isAttacking = true;
                    Invoke("StartAttack", attackDelay);
                }

                isPlayerInRange = true;
            }
            else
            {
                ResetAttack();
            }
        }
        else
        {
            ResetAttack();
        }
    }

    // B?t ??u t?n c�ng
    private void StartAttack()
    {
        // Th?c hi?n animation t?n c�ng
        if (isPlayerInRange)
        {
            anim.SetTrigger("SkeletonAttack");
            Invoke("AttackPlayer", 0.5f);  // G�y s�t th??ng sau 0.5s khi animation b?t ??u
        }
        else
        {
            ResetAttack();
        }
    }

    // G�y s�t th??ng cho ng??i ch?i
    private void AttackPlayer()
    {
        if (isPlayerInRange && playerLife != null)
        {
            playerLife.TakeDamage(1f);
        }

        // ??t l?i tr?ng th�i t?n c�ng
        isAttacking = false;
        attackArea.SetActive(false);  // ?n v�ng t?n c�ng sau khi ho�n th�nh
    }

    // ??t l?i tr?ng th�i t?n c�ng
    private void ResetAttack()
    {
        attackArea.SetActive(false);
        isPlayerInRange = false;
        isAttacking = false;
        anim.SetTrigger("enemyRun");
    }

    // Debug ?? v? ph?m vi t?n c�ng tr�n Scene (h�nh b�n nguy?t)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // V? h�nh b�n nguy?t
        Vector3 forward = transform.right;  // H??ng m� Skeleton ?ang nh�n (tr?c x)
        Vector3 startPoint = Quaternion.Euler(0, 0, -attackAngle / 2) * forward * attackRadius; // ?i?m b?t ??u

        Vector3 lastPoint = startPoint;
        for (int i = 1; i <= 30; i++) // V? b?ng c�ch t?o 30 ???ng th?ng ?? t?o th�nh cung tr�n
        {
            float angle = -attackAngle / 2 + (attackAngle / 30) * i;
            Vector3 nextPoint = Quaternion.Euler(0, 0, angle) * forward * attackRadius;
            Gizmos.DrawLine(transform.position + lastPoint, transform.position + nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.DrawLine(transform.position + lastPoint, transform.position);  // K?t n?i cu?i v? trung t�m
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        anim.SetBool("SkeletonDeath", true);
    }
}
