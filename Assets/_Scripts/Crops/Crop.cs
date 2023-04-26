using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CropType
{
    None,
    Tomato,
    Water,
    Milk
}
[CreateAssetMenu(menuName = "ScriptableObjects/Crop")]
public class Crop : ScriptableObject
{
    public string Name;
    public int Quantity;
    public CropType cropType;    
    public GameObject Prefab;
}
