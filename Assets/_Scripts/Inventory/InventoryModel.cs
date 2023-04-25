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
        m_Controller.FetchInventory(m_CropModelInfo);
    }

    public void UpdateInventoryData(int id, int amountChanged) 
    {
        for (int i = 0; i < m_CropModelInfo.Count; i++)
        {
            if (m_CropModelInfo[i].Id == id)
            {
                CropModelInfo temp = m_CropModelInfo[i];
                temp.m_Amount += amountChanged;
                m_CropModelInfo[i] = temp;
                m_Controller.OnInventoryUpdate(m_CropModelInfo[i]);
                break;
            }
        }
        
    }
}
