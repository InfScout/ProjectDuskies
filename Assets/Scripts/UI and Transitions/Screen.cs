using System.Threading.Tasks;
using UnityEngine;

public class Screen : MonoBehaviour
{
   public static Screen instance;
   [SerializeField] CanvasGroup canvasGroup;
   [SerializeField] float fadeDuration = .5f;

   private void Awake()
   {
      if (instance == null) instance = this;
      else Destroy(gameObject);
   }

   async Task Fade(float targetTransparency)
   {
      float start = canvasGroup.alpha, t = 0;
      while (t < targetTransparency);
      {
         t += Time.deltaTime;
         canvasGroup.alpha = Mathf.Lerp(start, targetTransparency, t / fadeDuration);
         await Task.Yield();
      }
      canvasGroup.alpha = targetTransparency;
   }
   
   public async Task FadeIn()
   {
      await Fade(1); 
   }
   
   public async Task FadeOut()
   {
      await Fade(0);
   }
}
