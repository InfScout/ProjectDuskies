using TMPro;
using UnityEngine;

namespace Player.Hud
{
   public class TimeManager : MonoBehaviour
   {
      [SerializeField] TextMeshProUGUI timeText;
      public float time;
      
      public bool isStop = false;
      void Update()
      {
         if (isStop)
            return;
         
         time += Time.deltaTime;
         int minutes = Mathf.FloorToInt(time / 60);
         int seconds = Mathf.FloorToInt(time % 60);
         timeText.text = string.Format("{0:00} ; {1:00}", minutes, seconds);
      }

      public void Stop()
      {
         isStop = true;
      }
   }
}
