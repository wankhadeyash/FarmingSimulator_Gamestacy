
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropLocationInfo 
{
    public GameObject Location;
    [HideInInspector] public CropController CurrentCropController;
    public bool IsOccupided;
}
public class LandController : MonoBehaviour
{
    Land m_Land = new Land();
    public CropType m_RequiredCropType;
    public CropType m_LandCropType;
    public Crop m_CropToPlant;
    public LandView m_LandView;
    public bool m_RequireSeeds;
    public List<CropLocationInfo> m_CropLocationInfo; // Location at which plants will be planted
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
                m_LandView.m_LandDisplayName.text = m_LandCropType.ToString();
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
        m_LandView.m_QuantityOfResourceText.text = Inventory.InventoryList.Find(x => x.cropType == m_RequiredCropType).amount.ToString();

    }

    //Object pooling crops
    void PoolCrops() 
    {
        for (int i = 0; i < m_CropLocationInfo.Count; i++) 
        {
            CropLocationInfo cropLocation = m_CropLocationInfo[i];
            cropLocation.CurrentCropController = Instantiate(m_CropToPlant.Prefab, m_CropLocationInfo[i].Location.transform.position, Quaternion.identity, m_CropLocationInfo[i].Location.transform).GetComponent<CropController>();
            cropLocation.CurrentCropController.m_LandController = this;
            if (i<2)
            {
                cropLocation.CurrentCropController.gameObject.SetActive(true);
                cropLocation.IsOccupided = true;
            }
            else
                cropLocation.CurrentCropController.gameObject.SetActive(false);
            m_CropLocationInfo[i] = cropLocation;
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

        //Empty out all the locations for plantation of new crops
        for (int i = 0; i < m_CropLocationInfo.Count; i++)
        {
            CropLocationInfo cropLocation = m_CropLocationInfo[i];
            if (cropLocation.IsOccupided && cropLocation.CurrentCropController.m_IsReadyToHarvest)
            {
                //Free crop location to plant for next iteration
                cropLocation.IsOccupided = false;
                cropLocation.CurrentCropController.ResetCrop();
                cropLocation.CurrentCropController.gameObject.SetActive(false);
                m_Land.SeedQuantity += Random.Range(1, 5);
                m_Land.ReadyToHarvestQuantity--;
                m_CropLocationInfo[i] = cropLocation;

                //Add Item in inventory
                Inventory.AddInventoryItem(m_LandCropType, 1);
            }
        }
        
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
        m_LandView.m_QuantityOfSeedsText.text = m_Land.SeedQuantity.ToString();

    }

    public void PlantCrop() 
    {
        //Cannot plant if no seeds or no resources are available in inventory
        if (m_Land.SeedQuantity <= 0 && m_RequireSeeds)
        {
            ErrorDisplay.DisplayError("Not Enough Seeds");
            return;
        }
        InventoryInfo inventoryInfo = Inventory.InventoryList.Find(x => x.cropType == m_RequiredCropType);
        if (inventoryInfo.amount <= 0)
        {
            ErrorDisplay.DisplayError($"Not enough {m_RequiredCropType}");
            return;
        }
        else 
        {
            Inventory.RemoveInventoryItem(m_RequiredCropType, 1);
        }
        
        for (int i = 0; i < m_CropLocationInfo.Count; i++) 
        {
            if (!m_CropLocationInfo[i].IsOccupided) 
            {
                CropLocationInfo cropLocation = m_CropLocationInfo[i];
                cropLocation.IsOccupided = true;
                cropLocation.CurrentCropController.gameObject.SetActive(true);
                m_CropLocationInfo[i] = cropLocation;
                m_Land.SeedQuantity--;
                m_LandView.m_QuantityOfSeedsText.text = m_Land.SeedQuantity.ToString();
                break;
            }
        }

    }
}
