using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Enum for audio track types
public enum AudioTrackType
{
    Background,
    UI
}

// Struct to hold audio source and its track type
[System.Serializable]
public struct AudioTypeInfo
{
    public AudioSource Source;
    public AudioTrackType Type;
}

// Singleton class that manages sound effects in the game
public class SoundManager : SingletonBase<SoundManager>
{
    // List to hold audio sources and their track types
    [SerializeField] List<AudioTypeInfo> m_AudioTrackInfoList = new List<AudioTypeInfo>();

    // Static method to play a sound clip on a specific audio track type
    public static void PlaySound(AudioClip clip, AudioTrackType track)
    {
        // Call the internal method on the singleton instance
        s_Instance.PlaySoundInternal(clip, track);
    }

    // Internal method to play a sound clip on a specific audio track type
    void PlaySoundInternal(AudioClip clip, AudioTrackType track)
    {
        // Find the audio source corresponding to the given track type
        AudioSource tempSource = m_AudioTrackInfoList.Find(x => x.Type == track).Source;

        // Set the clip to play on the audio source and play it
        tempSource.clip = clip;
        tempSource.Play();
    }
}