using System.Collections;
using Interfaces;
using UnityEngine;
namespace Enemy
{
    public class EnemyHealth : MonoBehaviour , IHittable
    {
        [SerializeField] private int maxHealth = 1;
        [SerializeField] private int currentHealth;
        
        [SerializeField] private float iframeTime = .75f;
        public bool isHittable = true;
        
        private void Start()
        {
            currentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            if (!isHittable) return;
            
            currentHealth -=  damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            StartCoroutine(IFrame());
            if (currentHealth <= 0)
            { 
                Destroy(gameObject);
                
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