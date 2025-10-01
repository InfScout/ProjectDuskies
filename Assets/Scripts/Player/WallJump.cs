using System;
using UnityEngine;

namespace Player
{
    public class WallJump : MonoBehaviour
    {
        private Rigidbody2D rb;
        private GroundCheck groundCheck;
        private WallCheck wallCheck;
        private PlayerMovement playerMovement;
        
        private bool _isWallJumping = false;
        [SerializeField] private float wallJumpDir ;
        [SerializeField] private float wallJumpTime = .5f;
        private float wallJumpTimer;
        [SerializeField] private Vector2 wallJumpPower = new Vector2(5f,10f);
        private bool _isWallSliding = false;
        [SerializeField] private float _wallSlideSpeed = 0.2f;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            wallCheck = GetComponent<WallCheck>();
            groundCheck = GetComponent<GroundCheck>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
           WallSlide();
        }

        private void WallSlide()
        {
            if (!groundCheck.IsGrounded() & wallCheck.IsWalled() )
            {
                _isWallSliding = true;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, MathF.Max(rb.linearVelocity.y, - _wallSlideSpeed));
            }
            else
            {
                _isWallSliding = false;
            }
        }
    }
}