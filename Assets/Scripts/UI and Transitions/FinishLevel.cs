using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour , IItem 
{
   
   public UnityEvent onFinish;


   private void Start()
   {
      onFinish.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().NextLevel);
   }
   
   public void Collect()
   {
       
        onFinish.Invoke();
        Destroy(gameObject);
   }
}
