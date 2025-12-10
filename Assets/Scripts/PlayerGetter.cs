using UnityEngine;

public class PlayerGetter : MonoBehaviour
{
 private static PlayerGetter instance;
 
 public Transform playerPosition;

 private void Awake()
 {
     instance = this;
 }
 public static Transform GetPlayerPosition()
 {
   return instance.playerPosition;
 }
 
 
}
