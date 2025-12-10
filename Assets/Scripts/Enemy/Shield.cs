using System;
using System.Collections;
using UnityEngine;
using Enemy;
using Player;


public class Shield : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;
    private EnemyHealth _enemy;
    private Dash _dash;
    
    public bool isHittable = false;
    [SerializeField] private float iframeTime = 1.2f;
    

    private void Awake()
    {
        currentHealth = maxHealth;
        _enemy = GetComponentInParent<EnemyHealth>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHittable) return;

        _dash = collision.GetComponent<Dash>();
        if (_dash && _dash.GetIsDashing())
        {
            _enemy.isHittable = false;
            ShieldTakeDamage();
        }
    }

    public void ShieldTakeDamage()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(IFrame());
        if (currentHealth <= 0)
        {
            _enemy.isHittable = true;
            Debug.Log(_enemy.isHittable);
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
