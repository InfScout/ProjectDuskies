using UnityEngine;
using Player;

namespace Enemy
{
    
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField]private PlayerHealth _playerHealth;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerHealth.TakeDamage(1);
            }
        }
    }
}