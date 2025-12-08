using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour , IHittable
    {
        [SerializeField] private int maxHealth = 1;
        [SerializeField] private int currentHealth;
        [SerializeField] private int scoreGive = 100;
        
        [SerializeField] private float iframeTime = .75f;
        public bool isHittable = true;
        public UnityEvent onDeath;
        
        private void Start()
        {
            onDeath.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().AddScore(scoreGive));
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