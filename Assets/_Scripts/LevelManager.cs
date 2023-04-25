using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static UnityAction<int> OnSceneChanged; // int-> Scene index
    int m_CurrentSceneId;
    public int CurrentSceneId => s_Instance.m_CurrentSceneId;
    private static LevelManager s_Instance;
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
        OnSceneChanged?.Invoke(sceneId);
    }
}
