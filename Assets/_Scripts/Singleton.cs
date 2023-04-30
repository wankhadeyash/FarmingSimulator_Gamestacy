using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Declare a static instance variable that will hold the single instance of the class.
    protected static T s_Instance;

    // Declare a public property that will allow external access to the instance variable.
    public T Instance => s_Instance;

    // Awake is called when the script instance is being loaded.
    protected virtual void Awake()
    {
        // If there is no existing instance of the class, set the instance to this instance.
        if (s_Instance == null)
        {
            s_Instance = this as T;
            // Ensure that the instance is not destroyed when loading new scenes.
            DontDestroyOnLoad(s_Instance);
        }
        // If there is an existing instance of the class, destroy this instance.
        else
        {
            Destroy(gameObject);
        }
    }

    // OnDestroy is called when the MonoBehaviour will be destroyed.
    protected virtual void OnDestroy()
    {
        // If the instance being destroyed is the current instance, set the instance to null.
        if (s_Instance == this)
        {
            s_Instance = null;
        }
    }
}