using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    public int Id;
    float m_GrowFactor;
    public float m_GrowSpeed;
    [HideInInspector] public bool m_IsReadyToHarvest;
    LandController m_LandController;
    private void Start()
    {
        var landControllers = FindObjectsOfType<LandController>();
        foreach (LandController controller in landControllers) 
        {
            if (Id == controller.Id) 
            {
                m_LandController = controller;
                m_LandController.m_CropControllers.Add(this);
                break;
            }
        }
        StartCoroutine(Co_GrowCrop());
    }   


    IEnumerator Co_GrowCrop() 
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        while (m_GrowFactor < 2) 
        {
            yield return new WaitForSecondsRealtime(0.1f);
            transform.localScale = new Vector3(m_GrowFactor, m_GrowFactor, m_GrowFactor);
            m_GrowFactor += Time.deltaTime * m_GrowSpeed;
        }
        m_IsReadyToHarvest = true;
        m_LandController.ReadyToHarvest();
    }

    public void HarvestCrop()
    {

      //  cropView.UpdateView(crop);
        Inventory inventory = FindObjectOfType<Inventory>();
        //inventory.AddCrop(m_Crop);
    }

}
