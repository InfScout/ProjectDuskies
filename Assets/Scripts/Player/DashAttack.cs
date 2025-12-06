using Enemy;
using UnityEngine;
using Interfaces;

namespace Player
{
    public class DashAttack : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        
        private Dash _dash;
        private EnemyHealth _enemyHealth;

      
        private void Start()
        {
            _dash = GetComponent<Dash>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_dash.GetIsDashing())
            {
                var enemy = other.GetComponent<IHittable>();
                if (enemy != null )
                {
                    enemy.TakeDamage(damage);
                    SoundManager.PlaySound("Hit");
                    _dash.AddDash();
                }
            }
        }
        
    }
}