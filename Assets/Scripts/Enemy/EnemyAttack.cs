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
        private void Awake()
        {
            if (_playerHealth == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null )
                {
                    _playerHealth = player.GetComponent<PlayerHealth>();
                    _dash = player.GetComponent<Dash>();
                }
            }
        }
        
        
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !_dash.GetIsDashing())
            {
                _playerHealth.TakeDamage(damage);
            }
        }
    }
}