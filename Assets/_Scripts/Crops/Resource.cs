using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Type of resourceType enum
public enum ResourceType
{
    None,
    Tomato,
    Watermeloen,
    Cabbage,
    Water,
    CowDung
}
[CreateAssetMenu(menuName = "ScriptableObjects/Resource")] // Scriptable Object which instantiates prefab for object pooling
// Object pooling is done in LandController.cs
public class Resource : ScriptableObject
{
    public string Name;
    public ResourceType resourceType;    
    public GameObject Prefab;
}
