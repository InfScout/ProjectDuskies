
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Dash : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private GroundCheck _groundCheck;
        private Animator _animator;
        
        private bool _canDash = true;
        public bool isDashing = false;
        [SerializeField] private float maxStamina = 31;
        [SerializeField] private float _currentStamina;
        private float _baseGravity;
        [SerializeField] private float dashRegenSpeed = 2.5f;
        [SerializeField] private float dashCost = 10;
        [SerializeField]private float dashPower = 24f;
        [SerializeField]private float dashTime = .2f;
        [SerializeField]private float dashCooldown = 1f;
        
        private TrailRenderer _trail;
        private Rigidbody2D _rb;

        private void Start()
        {
            _currentStamina =  maxStamina;
            _groundCheck = GetComponent<GroundCheck>();
            _playerMovement = GetComponent<PlayerMovement>();
            _trail = GetComponent<TrailRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_groundCheck.IsGrounded() && _currentStamina != maxStamina)
            {
                RegenStam();
            }
        }

        public void DashStart(InputAction.CallbackContext context)
        {
            if (context.performed & _canDash & _currentStamina > dashCost)
            {
                StartCoroutine(DashCoroutine());
            }
        }
        
        private IEnumerator DashCoroutine()
        {
            _animator.SetTrigger("Dashing");
            _canDash = false;
            isDashing = true;
            _currentStamina -= dashCost;
            
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

        private void RegenStam()
        {
            _currentStamina += dashRegenSpeed * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0f, maxStamina);
        }
    }
}