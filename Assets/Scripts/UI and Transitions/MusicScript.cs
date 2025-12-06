using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
   private static MusicScript instance;
   private AudioSource audioSource;
   public AudioClip BGMclip;
   [SerializeField] private Slider musicSlider;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         audioSource = GetComponent<AudioSource>();
         DontDestroyOnLoad(this);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   void Start()
   {
      if (BGMclip != null)
      {
         PlayBGM(false, BGMclip);
      }
      musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
   }
   

   public static void SetVolume(float volume)
   {
      instance.audioSource.volume = volume;
   }
   


   public static void PlayBGM(bool resetSong,AudioClip clip = null)
   {
      if (clip != null)
      {
         instance.audioSource.clip = clip;
      }
      if (instance.audioSource.clip != null)
      {
         if (resetSong)
         {
            instance.audioSource.Stop();
         }
         instance.audioSource.Play();
      }
   }

   public static void PausedBGM()
   {
      instance.audioSource.pitch = .75f;
   }

   public static void UnpausedBGM()
   {
      instance.audioSource.pitch = 1f;
   }
}
