using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] float moveSpeed = 5f;
    private float moveX, moveY;
    private Vector2 moveDir;
    private Vector2 mousePos;

    [Header("Dash setting")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    // Animation
    private enum MovementState { idle, move }
    private MovementState state = MovementState.idle;

    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        UpdateAnimationState();
        Shoot();
    }

    private void Move()
    {
        if (isDashing)
        {
            return;
        }

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);
        if (moveDir == Vector2.zero)
        {
            rb.velocity = new Vector2(rb.velocity.x + (sprite.flipX ? -dashSpeed : dashSpeed), rb.velocity.y);
        }
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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

    protected virtual void UpdateAnimationState()
    {
        this.state = MovementState.idle;

        if (moveX > 0f)
        {
            state = MovementState.move;
            sprite.flipX = false;
        }
        else if (moveX < 0f)
        {
            state = MovementState.move;
            sprite.flipX = true;
        }
        else if (moveY > 0f || moveY < 0f)
        {
            state = MovementState.move;
        }
        else
        {
            state = MovementState.idle;
        }
        anim.SetInteger("state", (int)state);
    }
}
