using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    protected static T s_Instance;
    public T Instance => s_Instance;
    protected virtual void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this as T;
            DontDestroyOnLoad(s_Instance);
        }
        else 
        {
            Destroy(gameObject);
        }

    }

    protected virtual void OnDestroy() 
    {
        if (s_Instance == this)
            s_Instance = null;
    }



}
