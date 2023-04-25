using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using these interfaces to limit the methods exposed to respective classes
public interface IInventoryModel // passed to Model
{
    void OnInventoryUpdate(List<CropModelInfo> cropInfo);
}

public interface IInventoryView // passed to view
{
    
}
public class InventoryController : MonoBehaviour,IInventoryModel,IInventoryView //MVC pattern to handle inventory
{
    public InventoryModel m_Model;
    public InventoryView m_View;


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

    public void OnInventoryUpdate(List<CropModelInfo> cropInfo)
    {
        //Update on UI
        foreach (CropModelInfo data in cropInfo) 
        {
            m_View.UpdateUIData(data.Id,data.m_Amount);
        }
    }
    #endregion
}
