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
    [SerializeField]private float bulletLifeTime = 5f;
    private Rigidbody2D _rb;
    
  
    
    private void OnEnable()
    {
       StartCoroutine(DestroyBullet()); 
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Destroy(gameObject);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
  
}
