using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Using these interfaces to limit the methods exposed to respective classes
public interface IInventoryModel // passed to Model
{
    void FetchInventory(List<CropModelInfo> cropInfo);

    void OnInventoryUpdate(CropModelInfo cropModelInfo);
}

public interface IInventoryView // passed to view
{
    void OnPlaceCropButtonPressed(int id, int amountChanged);
}
public class InventoryController : MonoBehaviour,IInventoryModel,IInventoryView //MVC pattern to handle inventory
{
    public InventoryModel m_Model;
    public InventoryView m_View;
    public static UnityAction<int, int,Crop> OnCropPlanted;// int-> Id of crop, int -> Amount planted, Crop-> CropScriptable object

    private void OnEnable()
    {
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;

    }

    private void OnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Initialize:
                m_Model.Initialize(this);
                m_View.Initialize(this);
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

    #region IInventoryModel

    public void FetchInventory(List<CropModelInfo> cropInfo)
    {
        //Update on UI
        foreach (CropModelInfo data in cropInfo) 
        {
            m_View.UpdateUIData(data.Id,data.m_Amount);
        }
    }

    public void OnInventoryUpdate(CropModelInfo cropModelInfo)
    {
        OnCropPlanted?.Invoke(cropModelInfo.Id, cropModelInfo.m_Amount,cropModelInfo.crop);
    }
    #endregion

    #region IInventoryView
    public void OnPlaceCropButtonPressed(int id, int amountChanged)
    {
        m_Model.UpdateInventoryData(id, amountChanged);
        
    }

  
    #endregion
}
