
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropLocationInfo 
{
    public GameObject Location;
    public CropController CurrentCropController;
    public bool IsOccupided;
}
public class LandController : MonoBehaviour
{
    public int Id;
    Land m_Land = new Land();
    public Crop m_CropToPlant;
    public LandView m_LandView;
    public List<CropLocationInfo> m_CropLocationInfo; // Location at which plants will be planted
    Animator m_Animator;
    [HideInInspector] public List<CropController> m_CropControllers = new List<CropController>();
    private void OnEnable()
    {
        //InventoryController.OnCropPlanted += OnCropPlanted;
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;

    }



    private void OnDisable()
    {
        // InventoryController.OnCropPlanted -= OnCropPlanted;
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;


    }
    private void OnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Initialize:
                m_Animator = GetComponent<Animator>();
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
            return;

        //Empty out all the locations for plantation of new crops
        for (int i = 0; i < m_CropLocationInfo.Count; i++)
        {
            CropLocationInfo cropLocation = m_CropLocationInfo[i];
            if (cropLocation.IsOccupided && cropLocation.CurrentCropController.m_IsReadyToHarvest)
            {
                cropLocation.IsOccupided = false;
                cropLocation.CurrentCropController = null;
                m_CropLocationInfo[i] = cropLocation;
            }
        }

        //Destroy the crops which are ready to harvest and handle the CropControllers List Accordingly
        for(int i = m_CropControllers.Count-1; i >= 0; i--) 
        {
            if (!m_CropControllers[i].m_IsReadyToHarvest)
                continue;
            else 
            {
                m_Land.SeedQuantity += Random.Range(1, 5);
                m_Land.ReadyToHarvestQuantity--;
                Destroy(m_CropControllers[i].gameObject);
                m_CropControllers.RemoveAt(i);
            }
            
        }
        
        m_LandView.m_QuantityOfReadToHarvestText.text = m_Land.ReadyToHarvestQuantity.ToString();
        m_LandView.m_QuantityOfSeeds.text = m_Land.SeedQuantity.ToString();

    }

    public void PlantCrop() 
    {
        if (m_Land.SeedQuantity<= 0)
            return;
        for (int i = 0; i < m_CropLocationInfo.Count; i++) 
        {
            if (!m_CropLocationInfo[i].IsOccupided) 
            {
                CropLocationInfo cropLocation = m_CropLocationInfo[i];
                cropLocation.IsOccupided = true;
                cropLocation.CurrentCropController = Instantiate(m_CropToPlant.Prefab, cropLocation.Location.transform.position, Quaternion.identity, cropLocation.Location.transform).GetComponent<CropController>();
                m_CropLocationInfo[i] = cropLocation;
                m_Land.SeedQuantity--;
                m_LandView.m_QuantityOfSeeds.text = m_Land.SeedQuantity.ToString();
                break;
            }
        }

    }
}
