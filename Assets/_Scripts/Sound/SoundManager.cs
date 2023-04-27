using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioTrackType 
{
    Background,
    UI
}

[System.Serializable]
public struct AudioTypeInfo 
{
    public AudioSource Source;
    public AudioTrackType Type;
}
public class SoundManager : MonoBehaviour
{
    public List<AudioTypeInfo> m_AudioTrackInfoList = new List<AudioTypeInfo>();
    AudioSource source;
    private static SoundManager s_Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void PlaySound(AudioClip clip, AudioTrackType track) 
    {
        s_Instance.PlaySoundInternal(clip, track);
    }
    void PlaySoundInternal (AudioClip clip, AudioTrackType track) 
    {
        AudioSource tempSource = m_AudioTrackInfoList.Find(x => x.Type == track).Source;
        tempSource.clip = clip;
        tempSource.Play();
    }
}