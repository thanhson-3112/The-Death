using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHome : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private TrailRenderer tr;

    [SerializeField] private float moveSpeed = 20f;
    private float moveX, moveY;
    private Vector2 moveDir;

    [Header("Dash setting")]
    [SerializeField] public float dashSpeed = 50f;
    [SerializeField] private float dashDuration = 0.2f;// khoang cach dash
    [SerializeField] private float dashCooldown = 0.5f;

    private bool isDashing;
    private bool canDash = true;


    // Animation
    private enum MovementState { idle, move }
    private MovementState state = MovementState.idle;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

    }

    private void Update()
    {
        Move();
        UpdateAnimationState();
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
        tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        tr.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
