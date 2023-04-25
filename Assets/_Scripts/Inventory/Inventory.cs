using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropModelInfo 
{
    public int Id;
    public Crop crop;
    public int m_Amount;
}
public class Inventory : MonoBehaviour
{
    public List<Crop> crops;

    public void AddCrop(Crop crop)
    {
        Crop existingCrop = crops.Find(c => c.name == crop.name);
        if (existingCrop != null)
        {
            existingCrop.Quantity += crop.Quantity;
        }
        else
        {
            crops.Add(crop);
        }
    }

    public void RemoveCrop(Crop crop)
    {
        Crop existingCrop = crops.Find(c => c.name == crop.name);
        if (existingCrop != null)
        {
            existingCrop.Quantity -= crop.Quantity;
            if (existingCrop.Quantity <= 0)
            {
                crops.Remove(existingCrop);
            }
        }
    }


}
