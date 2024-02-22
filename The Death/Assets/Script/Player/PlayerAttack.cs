using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private Vector2 mousePos;
    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)
        {
            anim.SetTrigger("attack");
            Instantiate(firePrefab, firingPoint.position, firingPoint.rotation);
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y,
            mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}