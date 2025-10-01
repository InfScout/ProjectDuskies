using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Dash : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private bool canDash = true;
        public bool isDashing = false;
        private int dashCount = 3;
        [SerializeField]private float dashPower = 24f;
        [SerializeField]private float dashTime = .2f;
        [SerializeField]private float dashCooldown = 1f;
        
        private TrailRenderer trail;
        private Rigidbody2D rb;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            trail = GetComponent<TrailRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void DashStart(InputAction.CallbackContext context)
        {
            if (context.performed && canDash)
            {
                StartCoroutine(DashCoroutine());
            }
        }


        private IEnumerator DashCoroutine()
        {
            canDash = false;
            isDashing = true;
            
            trail.emitting = true;
            float dashDirection = _playerMovement.isFacingRight ? -1f : 1f;
            
            rb.linearVelocity = new Vector2(dashDirection * dashPower, rb.linearVelocity.y);
            
            yield return new WaitForSeconds(dashTime);
            
            rb.linearVelocity = new Vector2(0f , rb.linearVelocity.y);
            trail.emitting = false;
            isDashing = false;
            
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
}