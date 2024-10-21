using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : EnemyLifeBase
{
    [SerializeField] protected float flyingMaxHealth = 1000f;
    [SerializeField] protected float flyingHealth;
    public float flyingDamage = 5f;

    public GameObject attackArea;  // V�ng attack area (l� m?t GameObject con c?a Enemy)
    public float attackRadius = 5f; // B�n k�nh v�ng t?n c�ng
    public float attackDelay = 0.5f;  // ?? tr? tr??c khi t?n c�ng
    public int damage = 10;         // L??ng s�t th??ng g�y ra khi t?n c�ng
    public float cooldownTime = 2f; // Th?i gian h?i tr??c khi t?n c�ng l?i
    private Transform player;
    private bool hasDetectedPlayer = false; // Ki?m tra xem ?� ph�t hi?n ng??i ch?i hay ch?a
    private bool isAttacking = false;
    private bool isOnCooldown = false;  // Bi?n ?? ki?m so�t th?i gian h?i chi�u

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
        // N?u ch?a ph�t hi?n ng??i ch?i, th� ki?m tra kho?ng c�ch ?? ph�t hi?n
        if (!hasDetectedPlayer)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Ph�t hi?n ng??i ch?i n?u h? ? trong b�n k�nh t?n c�ng
            if (distanceToPlayer <= attackRadius && !isOnCooldown)
            {
                hasDetectedPlayer = true;  // ?�nh d?u l� ?� ph�t hi?n ng??i ch?i
                attackArea.SetActive(true);  // Hi?n th? v�ng t?n c�ng

                // B?t ??u t?n c�ng sau m?t kho?ng tr?
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
        // Th?c hi?n animation t?n c�ng
        anim.SetTrigger("FlyingAttack");
        Invoke("AttackPlayer", 0.5f);  // G�y s�t th??ng sau 0.5s khi animation b?t ??u
    }

    private void AttackPlayer()
    {
        if (playerLife != null)
        {
            playerLife.TakeDamage(1f);  // G�y s�t th??ng cho ng??i ch?i
        }

        // ??t l?i tr?ng th�i t?n c�ng
        isAttacking = false;
        attackArea.SetActive(false);  // ?n v�ng t?n c�ng sau khi ho�n th�nh

        // B?t ??u th?i gian h?i chi�u tr??c khi c� th? t?n c�ng l?i
        StartCoroutine(StartCooldown());
    }

    // Coroutine ?? qu?n l� th?i gian h?i chi�u
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;  // ??t tr?ng th�i h?i chi�u
        anim.SetTrigger("FlyingRun");
        yield return new WaitForSeconds(cooldownTime);  // ??i th?i gian h?i chi�u
        isOnCooldown = false;  // Cho ph�p t?n c�ng l?i sau khi h?t th?i gian h?i
        hasDetectedPlayer = false;  // ??t l?i tr?ng th�i ph�t hi?n ng??i ch?i
    }

    // Debug ?? v? ph?m vi t?n c�ng tr�n Scene
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
