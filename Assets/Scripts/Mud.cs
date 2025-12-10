using UnityEngine;
using Player;

public class Mud : MonoBehaviour
{
  private PlayerMovement playerMovement;

  private void OnTriggerEnter2D(Collider2D other)
  {
    playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    if (playerMovement)
    {
        playerMovement.Mudded();
    }
  }
}
