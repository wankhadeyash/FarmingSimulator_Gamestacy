using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static UnityAction<int> OnLevelChanged; // int-> Scene index
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
        SceneManager.LoadScene(sceneId);
        m_CurrentSceneId = sceneId;
        
    }
}
