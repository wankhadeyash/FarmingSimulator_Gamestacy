using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare an enum named "ResourceType" to define the different types of resources available in the game.
public enum ResourceType
{
    None,           // Placeholder value
    Tomato,         // Type for tomatoes
    Watermelon,     // Type for watermelons
    Cabbage,        // Type for cabbages
    Water,          // Type for water
    CowDung         // Type for cow dung
}

// Use the CreateAssetMenu attribute to allow creation of ResourceData scriptable objects from the Unity Editor.
// This scriptable object is used to store data about resources for object pooling purposes.
[CreateAssetMenu(menuName = "ScriptableObjects/ResourceData")]
public class ResourceData : ScriptableObject
{
    // Declare public fields for resource data, such as the name of the resource, its type, and its prefab.
    public string Name;                 // Name of the resource
    public ResourceType resourceType;   // Type of the resource, as defined by the ResourceType enum
    public GameObject Prefab;           // Prefab object to be instantiated for object pooling
}