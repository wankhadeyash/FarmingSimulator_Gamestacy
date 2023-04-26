using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NaturalResource : MonoBehaviour
{
    public Canvas m_Canvas;
    public CropType m_LandCropType;
    public float m_ResourceGenerateInterval;
    public TextMeshProUGUI m_QuantityOfReadToHarvestText;
    public TextMeshProUGUI m_LandDisplayNameText;
    int m_ReadyToHarvestCount;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
        Inventory.OnInventoryUpdated += OnInventoryUpdated;

    }

   

    private void OnDisable()
    {
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;
        Inventory.OnInventoryUpdated -= OnInventoryUpdated;

    }

    private void OnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Initialize:
                m_Canvas.gameObject.SetActive(false);
                m_LandDisplayNameText.text = m_LandCropType.ToString();
                break;
            case GameState.Playing:
                StartCoroutine(Co_GenerateResource());
                break;
            case GameState.Paused:
                break;
            case GameState.Resume:
                break;
            default:
                break;

        }
    }
    IEnumerator Co_GenerateResource()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(m_ResourceGenerateInterval);
            m_ReadyToHarvestCount++;
            m_QuantityOfReadToHarvestText.text = m_ReadyToHarvestCount.ToString();
        }
    }

    public void HarvestAll()
    {
        if (m_ReadyToHarvestCount <= 0)
        {
            ErrorDisplay.DisplayError("Not Ready To Harvest");
            return;
        }
        else 
        {
            for (int i = 0; i < m_ReadyToHarvestCount; i++) 
            {
                Inventory.AddInventoryItem(m_LandCropType, 1);
            }
        }
        m_ReadyToHarvestCount = 0;
        m_QuantityOfReadToHarvestText.text = m_ReadyToHarvestCount.ToString();
    }
    private void OnInventoryUpdated()
    {
       Inventory.InventoryList.Find(x => x.cropType == m_LandCropType).amount.ToString();

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
