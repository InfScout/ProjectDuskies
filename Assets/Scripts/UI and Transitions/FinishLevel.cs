using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour , IItem 
{
   
   public UnityEvent onFinish;


   private void Start()
   {
      onFinish.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ShowRankDisplay);
   }
   
   public void Collect()
   {
       Debug.Log("finish level");
        onFinish.Invoke();
        Destroy(gameObject);
   }
}
