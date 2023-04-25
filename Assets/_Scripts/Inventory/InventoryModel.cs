using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropModelInfo 
{
    public int Id;
    public Crop m_Crop;
    public int m_Amount;
}
public class InventoryModel : MonoBehaviour
{
    public List<CropModelInfo> m_CropModelInfo;
    IInventoryModel m_Controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(IInventoryModel controller) 
    {
        m_Controller = controller;
        m_Controller.OnInventoryUpdate(m_CropModelInfo);
    }
}
