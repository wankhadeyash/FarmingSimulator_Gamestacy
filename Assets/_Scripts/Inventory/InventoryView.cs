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
    public List<CropViewlInfo> m_CropViewInfo;
    IInventoryView m_Controller;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(IInventoryView controller)
    {
        m_Controller = controller;
    }

    public void UpdateUIData(int id, int amount) 
    {
        CropViewlInfo view = m_CropViewInfo.Find(x => x.Id == id);
        view.AmountText.text = amount.ToString();
    }

    public void PlaceCropButtonPressed(int id) 
    {
        m_Controller.OnPlaceCropButtonPressed(id, -1);
    }
}
