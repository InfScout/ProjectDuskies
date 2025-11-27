using UnityEngine;
using Player;
using UnityEngine.Serialization;

namespace Enemy
{
    
    public class EnemyAttack : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private Dash _dash;
        [SerializeField]private int damage = 1;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerHealth = collision.GetComponent<PlayerHealth>();
            _dash = collision.GetComponent<Dash>();
            if (_playerHealth && !_dash.GetIsDashing())
            {
                _playerHealth.TakeDamage(damage);
            }
        }
    }
}