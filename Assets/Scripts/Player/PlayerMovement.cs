using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    bool isFacingRight = true;
    
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float horizontalMovement;
    private GroundCheck groundCheck;
    
    [SerializeField] private float _gravity =2f;
    [SerializeField] private float _maxFallSpeed = 18f;
    [SerializeField] private float _fallSpeedMultiplier = 2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }	

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);
    }

    void Update()
    {
        Gravity();
        Flip();
    }

    public void MoveMe(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && groundCheck.IsGrounded())
        {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);   
                
        }
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
