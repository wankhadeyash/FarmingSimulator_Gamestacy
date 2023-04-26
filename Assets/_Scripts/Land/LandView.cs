using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandView : MonoBehaviour
{
    public Canvas m_Canvas;
    public TextMeshProUGUI m_LandDisplayName;
    public TextMeshProUGUI m_QuantityOfReadToHarvestText;
    public TextMeshProUGUI m_QuantityOfSeedsText;
    public TextMeshProUGUI m_QuantityOfResourceText;

    private void OnEnable()
    {
        m_Canvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            m_Canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Canvas.gameObject.SetActive(false);
        }
    }
}
