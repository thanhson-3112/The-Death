using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : EnemyLifeBase
{
    [SerializeField] protected float flyingMaxHealth = 1000f;
    [SerializeField] protected float flyingHealth;
    public float flyingDamage = 5f;

    public GameObject attackArea;  // Vùng attack area (là m?t GameObject con c?a Enemy)
    public float attackRadius = 5f; // Bán kính vùng t?n công
    public float attackDelay = 0.5f;  // ?? tr? tr??c khi t?n công
    public int damage = 10;         // L??ng sát th??ng gây ra khi t?n công
    public float cooldownTime = 2f; // Th?i gian h?i tr??c khi t?n công l?i
    private Transform player;
    private bool hasDetectedPlayer = false; // Ki?m tra xem ?ã phát hi?n ng??i ch?i hay ch?a
    private bool isAttacking = false;
    private bool isOnCooldown = false;  // Bi?n ?? ki?m soát th?i gian h?i chiêu

    public override void Start()
    {
        flyingHealth = flyingMaxHealth;
        enemyMaxHealth = flyingMaxHealth;
        enemyHealth = flyingHealth;
        enemyDamage = flyingDamage;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackArea.SetActive(false);  // ?n attack area ban ??u

        base.Start();
    }

    private void Update()
    {
        // N?u ch?a phát hi?n ng??i ch?i, thì ki?m tra kho?ng cách ?? phát hi?n
        if (!hasDetectedPlayer)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Phát hi?n ng??i ch?i n?u h? ? trong bán kính t?n công
            if (distanceToPlayer <= attackRadius && !isOnCooldown)
            {
                hasDetectedPlayer = true;  // ?ánh d?u là ?ã phát hi?n ng??i ch?i
                attackArea.SetActive(true);  // Hi?n th? vùng t?n công

                // B?t ??u t?n công sau m?t kho?ng tr?
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
        // Th?c hi?n animation t?n công
        anim.SetTrigger("FlyingAttack");
        Invoke("AttackPlayer", 0.5f);  // Gây sát th??ng sau 0.5s khi animation b?t ??u
    }

    private void AttackPlayer()
    {
        if (playerLife != null)
        {
            playerLife.TakeDamage(1f);  // Gây sát th??ng cho ng??i ch?i
        }

        // ??t l?i tr?ng thái t?n công
        isAttacking = false;
        attackArea.SetActive(false);  // ?n vùng t?n công sau khi hoàn thành

        // B?t ??u th?i gian h?i chiêu tr??c khi có th? t?n công l?i
        StartCoroutine(StartCooldown());
    }

    // Coroutine ?? qu?n lý th?i gian h?i chiêu
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;  // ??t tr?ng thái h?i chiêu
        anim.SetTrigger("FlyingRun");
        yield return new WaitForSeconds(cooldownTime);  // ??i th?i gian h?i chiêu
        isOnCooldown = false;  // Cho phép t?n công l?i sau khi h?t th?i gian h?i
        hasDetectedPlayer = false;  // ??t l?i tr?ng thái phát hi?n ng??i ch?i
    }

    // Debug ?? v? ph?m vi t?n công trên Scene
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
