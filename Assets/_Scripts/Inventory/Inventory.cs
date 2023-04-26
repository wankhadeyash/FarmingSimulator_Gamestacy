using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public struct InventoryInfo
{
    public CropType cropType;
    public int amount;

}
public class Inventory : MonoBehaviour
{
    
    public static Inventory s_Instance;
    public static UnityAction OnInventoryUpdated;
    List<InventoryInfo> m_InventoryList = new List<InventoryInfo>();
    public static List<InventoryInfo> InventoryList => s_Instance.m_InventoryList;

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
                break;
            case GameState.Playing:
                BuildInventory();
                break;
            case GameState.Paused:
                break;
            case GameState.Resume:
                break;
            default:
                break;

        }
    }
    private void Start()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);
    }


    void BuildInventory() 
    {
        var values = Enum.GetValues(typeof(CropType));
        foreach (CropType type in values) 
        {
            InventoryInfo temp = new InventoryInfo { cropType = type, amount = 0 };
            m_InventoryList.Add(temp);
        }
    }

    public static void AddInventoryItem(CropType cropType, int amountAdded) 
    {
        s_Instance.AddInventoryItemInternal(cropType, amountAdded);
    }
    void AddInventoryItemInternal(CropType cropType,int amountAdded) 
    {
        for (int i = 0; i < m_InventoryList.Count; i++) 
        {
            if (m_InventoryList[i].cropType == cropType) 
            {
                InventoryInfo temp = m_InventoryList[i];
                temp.amount += amountAdded;
                m_InventoryList[i] = temp;
                break;
            }
        }
        OnInventoryUpdated?.Invoke();
    }
    public static void RemoveInventoryItem(CropType cropType, int amountRemoved)
    {
        s_Instance.RemoveInventoryItemInternal(cropType, amountRemoved);
    }
    void RemoveInventoryItemInternal(CropType cropType, int amountRemoved) 
    {
        for (int i = 0; i < m_InventoryList.Count; i++)
        {
            if (m_InventoryList[i].cropType == cropType)
            {
                InventoryInfo temp = m_InventoryList[i];
                temp.amount -= amountRemoved;
                m_InventoryList[i] = temp;
                break;
            }
        }
        OnInventoryUpdated?.Invoke();

    }
}
