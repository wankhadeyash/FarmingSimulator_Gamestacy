using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlantLocationInfo 
{
    public GameObject Location;
    public bool IsOccupided;
}
public class LandController : MonoBehaviour
{
    public int Id;
    public List<PlantLocationInfo> m_PlantLocationInfo; // Location at which plants will be planted
    private void OnEnable()
    {
        InventoryController.OnCropPlanted += OnCropPlanted;
    }



    private void OnDisable()
    {
        InventoryController.OnCropPlanted -= OnCropPlanted;

    }

    private void OnCropPlanted(int id, int amountPlanted,Crop crop)
    {
        if (Id != id)
            return;
        for (int i = 0; i < m_PlantLocationInfo.Count; i++) 
        {
            if (!m_PlantLocationInfo[i].IsOccupided) 
            {
                PlantLocationInfo temp =m_PlantLocationInfo[i];
                Instantiate(crop.Prefab, temp.Location.transform.position, Quaternion.identity, transform);
                temp.IsOccupided = true;
                m_PlantLocationInfo[i] = temp;
                break;
            }
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
