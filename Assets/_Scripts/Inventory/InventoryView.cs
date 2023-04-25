using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct CropViewlInfo
{
    public int Id;
    public TextMeshProUGUI AmountText;
}
public class InventoryView : MonoBehaviour
{
    public GameObject cropPrefab;
    public Transform cropParent;
    // Add any additional UI elements here

    public void UpdateView(List<Crop> crops)
    {
        foreach (Transform child in cropParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Crop crop in crops)
        {
            GameObject cropGO = Instantiate(cropPrefab, cropParent);
            CropView cropView = cropGO.GetComponent<CropView>();
            cropView.UpdateView(crop);
        }

        // Update any additional UI elements here
    }
}
