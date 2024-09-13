using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private TrailRenderer tr;

    private float moveX, moveY;
    private Vector2 moveDir;
    private float originalMoveSpeed;

    [Header("Dash setting")]
    [SerializeField] public float dashSpeed = 50f;
    [SerializeField] private float dashDuration = 0.2f;// khoang cach dash
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] public float dashMaxStamina = 2f;
    [SerializeField] public float dashStamina;

    private bool isDashing;
    private bool canDash = true;
    public DashBar dashBar;
    
    // Animation
    private enum MovementState { idle, move }
    private MovementState state = MovementState.idle;


    private PlayerPower playerPower;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        dashStamina = dashMaxStamina;
        dashBar.SetMaxDash(dashMaxStamina);
    }

    private void Update()
    {
        Move();
        UpdateAnimationState();
        // Hoi lai stamina theo thoi gian
        dashStamina += playerPower.playerCurrentAbilityHaste * Time.deltaTime;
        dashStamina = Mathf.Clamp(dashStamina, 0f, dashMaxStamina);
        dashBar.SetDash(dashStamina);
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

        if (Input.GetKeyDown(KeyCode.Space) && canDash && dashStamina >= 1)
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
        rb.velocity = new Vector2(moveDir.x * playerPower.playerCurrentSpeed, moveDir.y * playerPower.playerCurrentSpeed);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        dashStamina -= 1f;
        if (dashStamina < 0) dashStamina = 0;
        dashBar.SetDash(dashStamina);

        rb.velocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);
        if (moveDir == Vector2.zero)
        {
            rb.velocity = new Vector2(rb.velocity.x + (sprite.flipX ? -dashSpeed : dashSpeed), rb.velocity.y);
        }
        tr.emitting = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        tr.emitting = false;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

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