
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Player.Hud;
using UnityEngine.Serialization;

namespace Player
{
    public class Dash : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private GroundCheck _groundCheck;
        private Animator _animator;
        private TrailRenderer _trailRenderer;
        
        private bool _canDash = true;
        public bool isDashing;
        [SerializeField] private float maxStamina = 31;
        [SerializeField] private float currentStamina;
        private float _baseGravity;
        [SerializeField] private float dashRegenSpeed = 2.5f;
        [SerializeField] private float dashCost = 10;
        [SerializeField]private float dashPower = 24f;
        [SerializeField]private float dashTime = .2f;
        [SerializeField]private float dashCooldown = 1f;
        
        [SerializeField] private StamUIManager stamUIManager;
       
        private Rigidbody2D _rb;

        private void Start()
        {
            currentStamina =  maxStamina;
            _groundCheck = GetComponent<GroundCheck>();
            _playerMovement = GetComponent<PlayerMovement>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _trailRenderer.enabled = false;
        }

        private void Update()
        {
            if (_groundCheck.IsGrounded() && !Mathf.Approximately(currentStamina, maxStamina))
            {
                RegenStam();
            }
            stamUIManager.UpdateStamina( currentStamina ,  maxStamina);
        }
        

        public void DashStart(InputAction.CallbackContext context)
        {
            if (context.performed & _canDash & currentStamina > dashCost)
            {
                StartCoroutine(DashCoroutine());
            }
        }
        
        private IEnumerator DashCoroutine()
        {
            _animator.SetTrigger("Dashing");
            _canDash = false;
            isDashing = true;
            currentStamina -= dashCost;
            _trailRenderer.enabled = true;
            
            _baseGravity = _rb.gravityScale;
            _rb.gravityScale = 0;
            float dashDirection = _playerMovement.isFacingRight ? 1f : -1f;
            
            _rb.linearVelocity = new Vector2(dashDirection * dashPower, _rb.linearVelocity.y);
            
            yield return new WaitForSeconds(dashTime);
            
            _rb.linearVelocity = new Vector2(0f , _rb.linearVelocity.y);
            _rb.gravityScale = _baseGravity;
            isDashing = false;
            
            yield return new WaitForSeconds(dashCooldown);
            _trailRenderer.enabled = false;
            _canDash = true;
        }

        private void RegenStam()
        {
            currentStamina += dashRegenSpeed * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        public void AddDash()
        {
            currentStamina += dashCost;
            stamUIManager.UpdateStamina( currentStamina ,  maxStamina);
        }

        public bool GetIsDashing()
        {
            return isDashing;
        }
        
        
    }
}