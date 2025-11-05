using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
 [SerializeField]private GameObject _bullet;
 [SerializeField]private Transform _bulletSpawn;
 [SerializeField]private float _bulletSpawnSpeed = 5f;

 [SerializeField]private ShootingStrat _defautShootinStrat;
 private ShootingStrat _currentShootingStrat;

 private void Start()
 {
  SetShootingStrat(_defautShootinStrat);
 }
 public void Shooting(InputAction.CallbackContext context)
 {
  if (context.performed)
  {
   _currentShootingStrat?.Shoot(_bulletSpawn);
  }
 }

 public void SetShootingStrat(ShootingStrat newStrat)
 {
  _currentShootingStrat =  newStrat;
 
 }
 
}
