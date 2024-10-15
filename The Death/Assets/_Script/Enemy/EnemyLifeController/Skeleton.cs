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
    public float attackRadius = 3.11f;  // Bán kính vùng t?n công
    public float attackAngle = 144f;    // Góc m? c?a vùng bán nguy?t (180 ??)
    public float attackDelay = 0.5f;    // ?? tr? tr??c khi t?n công
    public int damage = 10;             // L??ng sát th??ng gây ra khi t?n công
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

        // N?u ng??i ch?i ? trong bán kính t?n công
        if (distanceToPlayer <= attackRadius)
        {
            // Ki?m tra góc gi?a h??ng Skeleton và v? trí ng??i ch?i
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer); // H??ng transform.right là h??ng Skeleton ?ang ??i m?t

            // N?u ng??i ch?i trong vùng bán nguy?t (góc)
            if (angleToPlayer <= attackAngle / 2)
            {
                if (!isAttacking)
                {
                    // Hi?n th? vùng t?n công
                    attackArea.SetActive(true);

                    // B?t ??u quá trình t?n công sau th?i gian tr?
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

    // B?t ??u t?n công
    private void StartAttack()
    {
        // Th?c hi?n animation t?n công
        if (isPlayerInRange)
        {
            anim.SetTrigger("SkeletonAttack");
            Invoke("AttackPlayer", 0.5f);  // Gây sát th??ng sau 0.5s khi animation b?t ??u
        }
        else
        {
            ResetAttack();
        }
    }

    // Gây sát th??ng cho ng??i ch?i
    private void AttackPlayer()
    {
        if (isPlayerInRange && playerLife != null)
        {
            playerLife.TakeDamage(1f);
        }

        // ??t l?i tr?ng thái t?n công
        isAttacking = false;
        attackArea.SetActive(false);  // ?n vùng t?n công sau khi hoàn thành
    }

    // ??t l?i tr?ng thái t?n công
    private void ResetAttack()
    {
        attackArea.SetActive(false);
        isPlayerInRange = false;
        isAttacking = false;
        anim.SetTrigger("enemyRun");
    }

    // Debug ?? v? ph?m vi t?n công trên Scene (hình bán nguy?t)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // V? hình bán nguy?t
        Vector3 forward = transform.right;  // H??ng mà Skeleton ?ang nhìn (tr?c x)
        Vector3 startPoint = Quaternion.Euler(0, 0, -attackAngle / 2) * forward * attackRadius; // ?i?m b?t ??u

        Vector3 lastPoint = startPoint;
        for (int i = 1; i <= 30; i++) // V? b?ng cách t?o 30 ???ng th?ng ?? t?o thành cung tròn
        {
            float angle = -attackAngle / 2 + (attackAngle / 30) * i;
            Vector3 nextPoint = Quaternion.Euler(0, 0, angle) * forward * attackRadius;
            Gizmos.DrawLine(transform.position + lastPoint, transform.position + nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.DrawLine(transform.position + lastPoint, transform.position);  // K?t n?i cu?i v? trung tâm
    }

    public override void EnemyDie()
    {
        base.EnemyDie();
        anim.SetBool("SkeletonDeath", true);
    }
}
