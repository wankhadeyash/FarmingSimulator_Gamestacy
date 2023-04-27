using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//View class for MVC pattern (Land.cs, LandView.cs, LandController)
//Responsible for displaying and updating data regarding respective land to user
public class LandView : MonoBehaviour
{
    public Canvas m_Canvas;
    public AudioClip m_UIOpenAudio;
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
            SoundManager.PlaySound(m_UIOpenAudio, AudioTrackType.UI);
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
