using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class SFXLibrary : MonoBehaviour
{
    
    [SerializeField] private SoundEffectGroup[] sfxGroup;
    private Dictionary<string, List<AudioClip>> soundDict;

    private void Awake()
    {
        soundDict = new Dictionary<string, List<AudioClip>>();
        foreach (SoundEffectGroup soundGroup in sfxGroup)
        {
            soundDict[soundGroup.name] = soundGroup.audioClips;
        }
    }

    public AudioClip GetRandomSFX(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDict[name];
            if (audioClips.Count > 0)
            {
                return audioClips[Random.Range(0, audioClips.Count)];
            }
        }
        return null;
    }
    
}


[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> audioClips;
}