using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Inventory Info data struct
public struct InventoryInfo
{
    public ResourceType resourceType;
    public int amount;
}

public class Inventory : SingletonBase<Inventory> // A data class from which every entity fetches respective data
{
    public static UnityAction OnInventoryUpdated;// Event fired if anychange in inventory

    List<InventoryInfo> m_InventoryList = new List<InventoryInfo>();

    public static List<InventoryInfo> InventoryList => s_Instance.m_InventoryList; // Property

    private void OnEnable()
    {
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;
    }

    // GameManager fires this event when there is a change in game state
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

    // Building Inventory on Start, i.e., adding all the possible inventory items to the list based upon ResourceType Enum values
    void BuildInventory()
    {
        var values = Enum.GetValues(typeof(ResourceType));
        foreach (ResourceType type in values)
        {
            InventoryInfo temp = new InventoryInfo { resourceType = type, amount = 0 };
            m_InventoryList.Add(temp);
        }
    }

    // Adds the specified amount of inventory item of the specified type to the inventory list and invokes the OnInventoryUpdated event.
    public static void AddInventoryItem(ResourceType cropType, int amountAdded)
    {
        s_Instance.AddInventoryItemInternal(cropType, amountAdded);
    }

    void AddInventoryItemInternal(ResourceType cropType, int amountAdded)
    {
        for (int i = 0; i < m_InventoryList.Count; i++)
        {
            if (m_InventoryList[i].resourceType == cropType)
            {
                InventoryInfo temp = m_InventoryList[i];
                temp.amount += amountAdded;
                m_InventoryList[i] = temp;
                break;
            }
        }
        OnInventoryUpdated?.Invoke();
    }

    // Removes the specified amount of inventory item of the specified type from the inventory list and invokes the OnInventoryUpdated event.
    public static void RemoveInventoryItem(ResourceType cropType, int amountRemoved)
    {
        s_Instance.RemoveInventoryItemInternal(cropType, amountRemoved);
    }

    void RemoveInventoryItemInternal(ResourceType cropType, int amountRemoved)
    {
        for (int i = 0; i < m_InventoryList.Count; i++)
        {
            if (m_InventoryList[i].resourceType == cropType)
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
