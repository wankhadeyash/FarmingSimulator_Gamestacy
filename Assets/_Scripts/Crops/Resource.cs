using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,
    Tomato,
    Watermeloen,
    Cabbage,
    Water,
    CowDung
}
[CreateAssetMenu(menuName = "ScriptableObjects/Resource")]
public class Resource : ScriptableObject
{
    public string Name;
    public ResourceType resourceType;    
    public GameObject Prefab;
}
