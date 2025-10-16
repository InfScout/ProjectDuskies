using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Transform _playerPosition;
    [SerializeField]private float _bulletSpeed = 10;
    [SerializeField]private int _bulletDamage = 1;
    [SerializeField]private float _bulletLifeTime = 5f;
    private Rigidbody2D _rb2D;

    private void OnEnable()
    {
       StartCoroutine(destroyBullet()); 
    }

    private IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(_bulletLifeTime);
        Destroy(gameObject);
    }
  
}
