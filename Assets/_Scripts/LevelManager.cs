using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Initialized on MainMenu scene
//Responsible for changing scenes
public class LevelManager : MonoBehaviour
{
    public static UnityAction<int> OnLevelChanged; // int-> Scene index // Whcn scene is changed fires the event
    public AudioClip m_ButtonPressAudio;
    int m_CurrentSceneId;
    public int CurrentSceneId => s_Instance.m_CurrentSceneId;
    private static LevelManager s_Instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        OnLevelChanged?.Invoke(scene.buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ChangeScene(int sceneId)
    {
        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        SceneManager.LoadScene(sceneId);
        m_CurrentSceneId = sceneId;
        
    }
    public void ExitGame() 
    {
        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        Application.Quit();
    }
}
