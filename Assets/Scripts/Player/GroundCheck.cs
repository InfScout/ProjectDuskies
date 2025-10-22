using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
       
        
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheckPos;
        [SerializeField] private Vector2 _groundCheckSize =  new Vector2(1f, 1f);
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public bool IsGrounded()
        {
            if (Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize, 0, _groundLayer))
            {
                return true;
                _animator.SetBool("isNotGrounded", true);
            }

            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_groundCheckPos.position, _groundCheckSize);
        }
    }
}