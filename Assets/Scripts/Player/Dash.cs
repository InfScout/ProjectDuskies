
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Dash : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private bool _canDash = true;
        public bool isDashing = false;
        private int _dashCount = 3;
        private float _baseGravity;
        [SerializeField]private float dashPower = 24f;
        [SerializeField]private float dashTime = .2f;
        [SerializeField]private float dashCooldown = 1f;
        
        private TrailRenderer _trail;
        private Rigidbody2D _rb;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _trail = GetComponent<TrailRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void DashStart(InputAction.CallbackContext context)
        {
            if (context.performed && _canDash)
            {
                StartCoroutine(DashCoroutine());
            }
        }


        private IEnumerator DashCoroutine()
        {
            _canDash = false;
            isDashing = true;
            
            _trail.emitting = true;
            _baseGravity = _rb.gravityScale;
            _rb.gravityScale = 0;
            float dashDirection = _playerMovement.isFacingRight ? 1f : -1f;
            
            _rb.linearVelocity = new Vector2(dashDirection * dashPower, _rb.linearVelocity.y);
            
            yield return new WaitForSeconds(dashTime);
            
            _rb.linearVelocity = new Vector2(0f , _rb.linearVelocity.y);
            _rb.gravityScale = _baseGravity;
            _trail.emitting = false;
            isDashing = false;
            
            yield return new WaitForSeconds(dashCooldown);
            _canDash = true;
        }
    }
}