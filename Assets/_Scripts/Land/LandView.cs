using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandView : MonoBehaviour
{
    public Canvas m_Canvas;
    public TextMeshProUGUI m_QuantityOfReadToHarvestText;
    public TextMeshProUGUI m_QuantityOfSeeds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
