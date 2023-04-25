using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Crop")]
public class Crop : ScriptableObject
{
    public string Name;
    public int Quantity;
    public int Id;
    public GameObject Prefab;
}
