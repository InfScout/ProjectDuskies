using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    
    private static SoundManager instance;
    
    private AudioSource audioSource;
    private static SFXLibrary sfxLibrary;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            sfxLibrary = GetComponent<SFXLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetRandomSFX(soundName);
        if (clip != null)
        {
            instance.audioSource.PlayOneShot(clip);
        }
    }

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate{ OnValueChanged();});
    }

    public static void SetVolume(float volume)
    {
        instance.audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(volumeSlider.value);
    }
}
