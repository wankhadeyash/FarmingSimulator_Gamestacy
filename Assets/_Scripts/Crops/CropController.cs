using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    float m_GrowFactor;
    public float m_GrowSpeed;
    [HideInInspector] public bool m_IsReadyToHarvest;
    [HideInInspector]  public LandController m_LandController;

    private void OnEnable()
    {
        
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

    public void ResetCrop() 
    {
        StopAllCoroutines();
        m_GrowFactor = 0;
        m_IsReadyToHarvest = false;
    }

}
