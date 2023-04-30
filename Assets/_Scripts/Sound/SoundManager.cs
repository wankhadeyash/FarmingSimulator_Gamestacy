using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Based on Tracks on AudioMixer
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

//Initialized on MainMenu scene
//Responsible for playing sounds
// Has a method with parameters (AudioClip, AudioTrackType) AudioClip - Clip to play AudioTrackType - At which track should be played on
public class SoundManager : Singleton<SoundManager>
{
    public List<AudioTypeInfo> m_AudioTrackInfoList = new List<AudioTypeInfo>();

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