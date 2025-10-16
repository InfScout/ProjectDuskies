using UnityEngine;
using Player;
using UnityEngine.Serialization;

namespace Enemy
{
    
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField]private PlayerHealth _playerHealth;
        [SerializeField]private int damage = 1;

        private void Awake()
        {
            if (_playerHealth == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    _playerHealth = player.GetComponent<PlayerHealth>();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerHealth.TakeDamage(damage);
            }
        }
    }
}