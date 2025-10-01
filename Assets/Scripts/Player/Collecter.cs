using UnityEngine;

public class Collecter : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      IItem item = other.GetComponent<IItem>();
      if (item != null)
      {
         item.Collect();
      }
   }
}
