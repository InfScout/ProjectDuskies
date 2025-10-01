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
    [SerializeField] private int maxJumps = 2;
    private int _currentJumps;
    
    
    
    private float wallJumpDir ;
    private float wallJumpTime = .2f; 
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

    void Awake()
    {
        _currentJumps = maxJumps;
    }
   

    void Update()
    {
        if (dash.isDashing || _isWallJumping)
        {
            return;
        }
        
        JumpReset();
        IsWallJump();
        Gravity();
        WallSlide();
        
        if (!_isWallJumping && !dash.isDashing)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);
            Flip();
        }
    }

    public void MoveMe(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void JumpReset()
    {
       if (groundCheck.IsGrounded() || _isWallSliding)
       {
           _currentJumps = maxJumps;
       }
    }
    
    public void Jump (InputAction.CallbackContext context)                                                 //jump
    {
        if (_currentJumps > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);   
                _currentJumps--;
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * .5f);
                _currentJumps--;
            } 
        }
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (context.performed && wallJumpTimer > 0f)
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
            Invoke(nameof(CancelWallJump),wallJumpTime);
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
            wallJumpTimer -= Time.deltaTime;
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
