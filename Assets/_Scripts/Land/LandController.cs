
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Data about physical location of Resource to plant and current status of location i.e is occupied or not
public struct ResourceLocationInfo 
{
    public GameObject Location;
    [HideInInspector] public ResourceController CurrentResourceController;
    public bool IsOccupided;
}

//Controller class for MVC pattern (Land.cs, LandView.cs, LandController)
// Also uses object pooling to pool the resource
public class LandController : MonoBehaviour
{
    Land m_Land = new Land(); // MVC data class

    public AudioClip m_ButtonPressAudio;

    public ResourceType m_RequiredResourceType; //Which other resouce is required to grow resource on this land
    public ResourceType m_LandResourceType; //Current land growing resource type

    public Resource m_ResourceToPlant;// Scriptable object which resource to plant

    public LandView m_LandView;// MVC view

    public bool m_RequireSeeds; // Does it requires seeds or can be grown based on required resource only

    public List<ResourceLocationInfo> m_ResourceLocationInfo; // Location at which plants will be planted

    Animator m_Animator;
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

    //Whenever change in inventory-> look for change 
    private void OnInventoryUpdated()
    {
        m_LandView.m_QuantityOfResourceText.text = Inventory.InventoryList.Find(x => x.resourceType == m_RequiredResourceType).amount.ToString();

    }

    //Resource Object pooling
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

    //Called from respective resourceController letting land controller know that it is ready to harvest
    public void ReadyToHarvest()
    {
        m_Animator.Play("ReadyToHarvest");
        m_Land.ReadyToHarvestQuantity++;
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
    }

    //Harvest all ready to harvest resources
    public void HarvestAll() 
    {
        if (m_Land.ReadyToHarvestQuantity <= 0)
        {
            MessageDisplay.DisplayMessage("Not Ready To Harvest");
            return;
        }

        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        //Empty out all the locations for plantation of new crops //Object pooling
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
        
        //MVC view Update
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
        m_LandView.m_QuantityOfSeedsText.text = m_Land.SeedQuantity.ToString();

    }

    public void PlantCrop() 
    {
        //Cannot plant if no seeds or no resources are available in inventory
        SoundManager.PlaySound(m_ButtonPressAudio, AudioTrackType.UI);
        if (m_Land.SeedQuantity <= 0 && m_RequireSeeds)
        {
            MessageDisplay.DisplayMessage("Not Enough Seeds");
            return;
        }
        InventoryInfo inventoryInfo = Inventory.InventoryList.Find(x => x.resourceType == m_RequiredResourceType);
        if (inventoryInfo.amount <= 0)
        {
            MessageDisplay.DisplayMessage($"Not enough {m_RequiredResourceType}");
            return;
        }
        else 
        {
            Inventory.RemoveInventoryItem(m_RequiredResourceType, 1);
        }
        
        //Used pooled objects to display resource plantation
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
