using UnityEngine;
[CreateAssetMenu(fileName = "Plume", menuName = "Turret/Plume")]

public class Plume : ShootingStrat
{
    public GameObject projectile;
    public GameObject projectileHitBox;
    public float plumeDuration;
    private GameObject _currentProjectileObj;
    private GameObject _curentProjectileHitBox;
    public override void Shoot(Transform shootPoint)
    {
      if (_currentProjectileObj) return;
      
      _currentProjectileObj = Instantiate(projectile , shootPoint.position , shootPoint.rotation);
      _curentProjectileHitBox = Instantiate(projectileHitBox, shootPoint.position, shootPoint.rotation);
      Destroy(_curentProjectileHitBox, plumeDuration);
      Destroy(_currentProjectileObj, plumeDuration);
    }
}
