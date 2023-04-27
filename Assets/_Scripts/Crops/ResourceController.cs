using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to grow resource at runtime by changing scale of the object
public class ResourceController : MonoBehaviour
{
    float m_GrowFactor;
    public float m_GrowSpeed;
    [HideInInspector] public bool m_IsReadyToHarvest;
    [HideInInspector]  public LandController m_LandController; //Land Contoller upon respective land the Resource is planted

    private void OnEnable()
    {
        StartCoroutine(Co_GrowResource());

    }
    IEnumerator Co_GrowResource() 
    {
        transform.localScale = Vector3.zero;
        while (m_GrowFactor < 2) 
        {
            yield return new WaitForSecondsRealtime(0.1f);
            transform.localScale = new Vector3(m_GrowFactor, m_GrowFactor, m_GrowFactor);
            m_GrowFactor += Time.deltaTime * m_GrowSpeed;
        }
        m_IsReadyToHarvest = true;
        m_LandController.ReadyToHarvest();
    }


    //Reset via object pooling
    public void ResetResource() 
    {
        StopAllCoroutines();
        m_GrowFactor = 0;
        m_IsReadyToHarvest = false;
    }

}
