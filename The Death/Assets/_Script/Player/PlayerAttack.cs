using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 mousePos;
    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private Transform firing;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 1f;
    private float fireTimer;

    private PlayerMovement playerMovement;
    [SerializeField] private float slowdownAmount = 10f;

    private bool autoAttacking = false; // Kiểm soát tấn công tự động

    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        if (!autoAttacking)
        {
            MouseAttack();
        }
        Shoot();
    }

    public void UpgradeAttackSpeed()
    {
        fireRate = fireRate - 0.1f;
        if(fireRate < 0.3)
        {
            fireRate = 0.3f;
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Chuyển đổi trạng thái tấn công tự động
            autoAttacking = !autoAttacking;
        }

        if ((autoAttacking || Input.GetMouseButtonDown(0)) && fireTimer <= 0f)
        {
            anim.SetTrigger("attack");
            fireTimer = fireRate;
            playerMovement.SlowDown(slowdownAmount);

            if (!autoAttacking)
            {
                Instantiate(firePrefab, firing.position, firing.rotation);
                MouseAttack();
            }
            else
            {
                AutoAttackNearestEnemy();
            }

        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (fireTimer <= 0f)
        {
            playerMovement.RestoreSpeed();
        }
    }

    private void AutoAttackNearestEnemy()
    {
        // Lấy tất cả các đối tượng Enemy trong phạm vi cố định
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            // Tính toán góc bắn viên đạn tới kẻ địch gần nhất
            Vector2 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Bắn viên đạn
            Instantiate(firePrefab, firing.position, Quaternion.Euler(0, 0, angle));
        }
    }

    private void MouseAttack()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y,
                        mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        firingPoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
