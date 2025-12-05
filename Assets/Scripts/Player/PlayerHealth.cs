using System;
using System.Collections;
using UnityEngine;
using Player.Hud;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField]private int health;
        [SerializeField]private HealthUIManager healthUIManager;
        [SerializeField] private float iframeTime = .75f;
        private Animator animator;
        public bool isHittable = true;
        
        public UnityEvent onDeath;
        private void Start()
        {
            onDeath.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Death);
            animator = GetComponent<Animator>();
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
                
                animator.SetTrigger("TakeDamage");
                
                healthUIManager.UpdateHealth();
                StartCoroutine(IFrame());
                
                if (health <= 0)
                {
                    onDeath.Invoke();
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