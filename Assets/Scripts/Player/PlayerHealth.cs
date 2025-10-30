using System;
using System.Collections;
using UnityEngine;
using Player.Hud;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField]private int health;
        [SerializeField]private HealthUIManager healthUIManager;
        [SerializeField] private float iframeTime = .75f;
        public bool isHittable = true;
        private void Start()
        {
            health = maxHealth;
        }

        public void HealthUp(int healAmount)
        {
            health += healAmount;
            health = Mathf.Clamp(health, 0, maxHealth);
            healthUIManager.RestoreHealth();
        }

        public void TakeDamage(int damage)
        {
            if (!isHittable) return;
            
                health -=  damage;
                health = Mathf.Clamp(health, 0, maxHealth);
                
                healthUIManager.UpdateHealth();
                StartCoroutine(IFrame());
                
                if (health <= 0)
                {
                    
                    //die
                }
        }

        private IEnumerator IFrame()
        {
            isHittable = false;
            yield return new WaitForSeconds(iframeTime);
            isHittable =  true;
        }
        
    }
}