
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResourceLocationInfo 
{
    public GameObject Location;
    [HideInInspector] public ResourceController CurrentResourceController;
    public bool IsOccupided;
}
public class LandController : MonoBehaviour
{
    Land m_Land = new Land();
    public AudioClip m_ButtonPressAudio;
    public ResourceType m_RequiredResourceType;
    public ResourceType m_LandResourceType;
    public Resource m_ResourceToPlant;
    public LandView m_LandView;
    public bool m_RequireSeeds;
    public List<ResourceLocationInfo> m_ResourceLocationInfo; // Location at which plants will be planted
    Animator m_Animator;
    private void OnEnable()
    {
        //InventoryController.OnCropPlanted += OnCropPlanted;
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
        Inventory.OnInventoryUpdated += OnInventoryUpdated;
    }



    private void OnDisable()
    {
        // InventoryController.OnCropPlanted -= OnCropPlanted;
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
                m_Animator = GetComponent<Animator>();
                m_LandView.m_LandDisplayName.text = m_LandResourceType.ToString();
                PoolCrops();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Resume:
                break;
            default:
                break;

        }
    }

    private void OnInventoryUpdated()
    {
        m_LandView.m_QuantityOfResourceText.text = Inventory.InventoryList.Find(x => x.resourceType == m_RequiredResourceType).amount.ToString();

    }

    //Object pooling resource
    void PoolCrops() 
    {
        for (int i = 0; i < m_ResourceLocationInfo.Count; i++) 
        {
            ResourceLocationInfo resourceLocation = m_ResourceLocationInfo[i];
            resourceLocation.CurrentResourceController = Instantiate(m_ResourceToPlant.Prefab, m_ResourceLocationInfo[i].Location.transform.position, Quaternion.identity, m_ResourceLocationInfo[i].Location.transform).GetComponent<ResourceController>();
            resourceLocation.CurrentResourceController.m_LandController = this;
            if (i<2)
            {
                resourceLocation.CurrentResourceController.gameObject.SetActive(true);
                resourceLocation.IsOccupided = true;
            }
            else
                resourceLocation.CurrentResourceController.gameObject.SetActive(false);
            m_ResourceLocationInfo[i] = resourceLocation;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadyToHarvest()
    {
        m_Animator.Play("ReadyToHarvest");
        m_Land.ReadyToHarvestQuantity++;
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
    }

    public void HarvestAll() 
    {
        if (m_Land.ReadyToHarvestQuantity <= 0)
        {
            ErrorDisplay.DisplayError("Not Ready To Harvest");
            return;
        }
        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        //Empty out all the locations for plantation of new crops
        for (int i = 0; i < m_ResourceLocationInfo.Count; i++)
        {
            ResourceLocationInfo resourceLocation = m_ResourceLocationInfo[i];
            if (resourceLocation.IsOccupided && resourceLocation.CurrentResourceController.m_IsReadyToHarvest)
            {
                //Free resource location to plant for next iteration
                resourceLocation.IsOccupided = false;
                resourceLocation.CurrentResourceController.ResetResource();
                resourceLocation.CurrentResourceController.gameObject.SetActive(false);
                m_Land.SeedQuantity += Random.Range(1, 5);
                m_Land.ReadyToHarvestQuantity--;
                m_ResourceLocationInfo[i] = resourceLocation;

                //Add Item in inventory
                Inventory.AddInventoryItem(m_LandResourceType, 1);
            }
        }
        
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
        m_LandView.m_QuantityOfSeedsText.text = m_Land.SeedQuantity.ToString();

    }

    public void PlantCrop() 
    {
        //Cannot plant if no seeds or no resources are available in inventory
        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        if (m_Land.SeedQuantity <= 0 && m_RequireSeeds)
        {
            ErrorDisplay.DisplayError("Not Enough Seeds");
            return;
        }
        InventoryInfo inventoryInfo = Inventory.InventoryList.Find(x => x.resourceType == m_RequiredResourceType);
        if (inventoryInfo.amount <= 0)
        {
            ErrorDisplay.DisplayError($"Not enough {m_RequiredResourceType}");
            return;
        }
        else 
        {
            Inventory.RemoveInventoryItem(m_RequiredResourceType, 1);
        }
        
        for (int i = 0; i < m_ResourceLocationInfo.Count; i++) 
        {
            if (!m_ResourceLocationInfo[i].IsOccupided) 
            {
                ResourceLocationInfo resourceLocation = m_ResourceLocationInfo[i];
                resourceLocation.IsOccupided = true;
                resourceLocation.CurrentResourceController.gameObject.SetActive(true);
                m_ResourceLocationInfo[i] = resourceLocation;
                m_Land.SeedQuantity--;
                m_LandView.m_QuantityOfSeedsText.text = m_Land.SeedQuantity.ToString();
                break;
            }
        }

    }
}
