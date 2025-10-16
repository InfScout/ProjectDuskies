using UnityEngine;

public abstract class ShootingStrat : ScriptableObject
{
    public string ShootingStratigyName;
    public GameObject shootingObj;
    
    

    public abstract void Shoot(Transform shootPoint);
    
}
