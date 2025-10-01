using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool isFacingRight = true;
    
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float horizontalMovement;
    private GroundCheck groundCheck;
    private WallCheck wallCheck;
    private Dash dash;
    
    [SerializeField] private float _gravity =2f;
    [SerializeField] private float _maxFallSpeed = 18f;
    [SerializeField] private float _fallSpeedMultiplier = 2f;


    private bool _isWallJumping;
    private float wallJumpDir ;
    private float wallJumpTime = .5f; 
    private float wallJumpTimer;
    [SerializeField] private Vector2 wallJumpPower = new Vector2(10f,15f);
    private bool _isWallSliding = false;
    [SerializeField] private float _wallSlideSpeed = 0.2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallCheck = GetComponent<WallCheck>();
        groundCheck = GetComponent<GroundCheck>();
        dash = GetComponent<Dash>();
    }	

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);
    }

    void Update()
    {
        if (dash.isDashing)
        {
            return;
        }
        IsWallJump();
        Gravity();
        WallSlide();
        if (!_isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);
            Flip();
        }
    }

    public void MoveMe(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    
    public void Jump (InputAction.CallbackContext context)                                                 //jump
    {
        if (context.performed && groundCheck.IsGrounded())
        {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);   
        }
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (context.performed && _isWallSliding)
        {
            _isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDir * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            if (transform.localScale.x != wallJumpDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1;
                transform.localScale = ls;
            }
            Invoke(nameof(CancelWallJump),wallJumpTime +0.1f);
        }
    }

    
    private void WallSlide()
    {
        if (!groundCheck.IsGrounded() & wallCheck.IsWalled() & horizontalMovement != 0)
        {
            _isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, MathF.Max(rb.linearVelocity.y, - _wallSlideSpeed));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void IsWallJump()
    {
        if (_isWallSliding)
        {
            _isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            
            CancelInvoke(nameof(CancelWallJump));
        }
        else if(wallJumpTimer > 0f)
        {
            wallJumpTime -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        _isWallJumping = false;
    }

    
    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = _gravity * _fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,Mathf.Max(rb.linearVelocity.y, - _maxFallSpeed));
        }
        else
        {
            rb.gravityScale = _gravity;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
}
