using System;
using UnityEngine;
using Player;
using UnityEngine.Events;

namespace Items
{
    public class HealthPickups : MonoBehaviour , IItem
    {
        [SerializeField]private PlayerHealth _playerHealth;
        [SerializeField]private int healAmmount = 1;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerHealth = collision.GetComponent<PlayerHealth>();
            if (_playerHealth)
                _playerHealth.HealthUp(healAmmount);
        }
        public void Collect()
        {
            
            Destroy(gameObject);
        }
    }
}