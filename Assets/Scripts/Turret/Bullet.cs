using System.Collections;
using UnityEngine;
using Player;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private Dash _dash;
    
    [SerializeField]private Transform _playerPosition;
    [SerializeField]private float _bulletSpeed = 10;
    [SerializeField]private int damage = 1;
    [SerializeField]private float bulletLifeTime = 5f;
    private Rigidbody2D _rb2D;
    
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
    
    private void OnEnable()
    {
       StartCoroutine(DestroyBullet()); 
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_dash.GetIsDashing())
        {
            _playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
  
}
