using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        private int health;

        private void Start()
        {
            health = maxHealth;
        }

        public void HealthUp()
        {
            health++;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        public void TakeDamage(int damage)
        {
                health -=  damage;
                if (health <= 0)
                {
                    //die
                }
        }
        
    }
}