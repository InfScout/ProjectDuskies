using UnityEngine;
[CreateAssetMenu(fileName = "RedBall", menuName = "Turret/RedBall")]

public class RedBall : ShootingStrat
{
    public float shootForce = 10;
    public override void Shoot(Transform shootPoint)
    {
        var fireBullet = Instantiate(shootingObj, shootPoint.position, shootPoint.rotation);
        fireBullet.GetComponent<Rigidbody2D>()?.AddForce(shootForce * shootPoint.forward, ForceMode2D.Impulse);
    }
}
