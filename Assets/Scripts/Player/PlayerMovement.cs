using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public bool isFacingRight = true;
    
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float _horizontalMovement;
    private GroundCheck _groundCheck;
    private WallCheck _wallCheck;
    private Dash _dash;
    
    [SerializeField] private float gravity =2f;
    [SerializeField] private float maxFallSpeed = 18f;
    [SerializeField] private float fallSpeedMultiplier = 2f;


    private bool _isWallJumping;
    [SerializeField] private int maxJumps = 2;
    private int _currentJumps;
    
    
    
    private float _wallJumpDir ;
    private float _wallJumpDuration = .2f; 
    private float _wallJumpTimer;
    [SerializeField] private Vector2 wallJumpPower = new Vector2(10f,15f);
    private bool _isWallSliding = false;
    [SerializeField] private float wallSlideSpeed = 0.2f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _wallCheck = GetComponent<WallCheck>();
        _groundCheck = GetComponent<GroundCheck>();
        _dash = GetComponent<Dash>();
    }

    void Awake()
    {
        _currentJumps = maxJumps;
    }
   

    void Update()
    {
        if (_dash.isDashing || _isWallJumping)
        {
            return;
        }
        
        JumpReset();
        IsWallJump();
        Gravity();
        WallSlide();
        
        if (!_isWallJumping && !_dash.isDashing)
        {
            _rb.linearVelocity = new Vector2(_horizontalMovement * speed, _rb.linearVelocity.y);
            Flip();
        }
    }

    public void MoveMe(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void JumpReset()
    {
       if (_groundCheck.IsGrounded() || _isWallSliding)
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
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);   
                _currentJumps--;
            }
            else if (context.canceled)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * .5f);
                _currentJumps--;
            } 
        }
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (context.performed && _wallJumpTimer > 0f)
        {
            _isWallJumping = true;
            _rb.linearVelocity = new Vector2(_wallJumpDir * wallJumpPower.x, wallJumpPower.y);
            _wallJumpTimer = 0;
            
            if (transform.localScale.x != _wallJumpDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1;
                transform.localScale = ls;
            }
            Invoke(nameof(CancelWallJump),_wallJumpDuration);
        }
    }

    
    private void WallSlide()
    {
        if (!_groundCheck.IsGrounded() & _wallCheck.IsWalled() & _horizontalMovement != 0)
        {
            _isWallSliding = true;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, MathF.Max(_rb.linearVelocity.y, - wallSlideSpeed));
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
            _wallJumpDir = -transform.localScale.x;
            _wallJumpTimer = _wallJumpDuration;
            
            CancelInvoke(nameof(CancelWallJump));
        }
        else if(_wallJumpTimer > 0f)
        {
            _wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        _isWallJumping = false;
    }

    
    private void Gravity()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = gravity * fallSpeedMultiplier;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x,Mathf.Max(_rb.linearVelocity.y, - maxFallSpeed));
        }
        else
        {
            _rb.gravityScale = gravity;
        }
    }

    private void Flip()
    {
        if (isFacingRight && _horizontalMovement < 0 || !isFacingRight && _horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
}
