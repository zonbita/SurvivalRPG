using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public struct InventoryItem
{
    public ItemSO Data;
    public int Quantity;
    public int Duration;
}

[System.Serializable]
public class InventoryManager : MonoBehaviour
{

    public InventoryItem[] inventoryItem;
    public InventoryItem[] InventoryItem => inventoryItem;
    public int inventorySize => InventoryItem.Length;

    //bool isFull = false;

    // Event
    public System.Action<InventoryManager,int> OnInventoryChangeSlot;
    public System.Action<InventoryManager, int, int> OnInventoryChange2Slot;
    public Action OnInit;

    public virtual void InitInventory(int size, ItemSO[] item, int quantity)
    {

        inventoryItem = new InventoryItem[size];

        if (item == null) return;

        if (item.Length == 0) return;

        for (int i = 0; i < item.Length - 1; i++)
        {
            if (item[i] != null && InventoryItem[i].Data)
            inventoryItem[i].Data = item[i];
            inventoryItem[i].Quantity = quantity;
        }
        OnInit?.Invoke();
    }

    public bool CheckFull()
    {
        foreach (InventoryItem item in inventoryItem)
        {
            if (item.Quantity < item.Data.MaxStack) return false;
        }
        return true;
    }

    public int GetEmptySlot()
    {

        for (int i = 0; i < inventoryItem.Length - 1; i++)
        {
            if (inventoryItem[i].Data == null) return i;

            if (inventoryItem[i].Data.ItemID == EItemID.Null) return i;
        }
        return -1;
    }

    public void AddMore(ItemSO item, int quantity)
    {
        int q = quantity;

        for (int i = 0; i < inventoryItem.Length - 1; i++)
        {
            if (inventoryItem[i].Data.ItemID == item.ItemID)
            {
                int total = quantity + inventoryItem[i].Quantity;
                if (total > item.MaxStack)
                {
                    inventoryItem[i].Quantity = item.MaxStack;
                    quantity = total - item.MaxStack;
                    if (quantity <= 0) break;
                }
                else
                    inventoryItem[i].Quantity += quantity;
            }
        }
    }

    public void Add(ItemSO item, int quantity)
    {
        if (item.ItemID == EItemID.Null) return;

        int index = GetEmptySlot();

        if (index != -1)
        {
            inventoryItem[index].Data = item;
            inventoryItem[index].Quantity = quantity;
            OnInventoryChangeSlot?.Invoke(this, index);
        }
    }

    public void SwitchItem(int index1, int index2)
    {
        if(inventoryItem[index1].Data != null)
        {
            if (inventoryItem[index2].Data != null)
            {
                
                if (inventoryItem[index1].Data.ItemID != inventoryItem[index2].Data.ItemID)
                {
                    SwapItems(index1, index2);
                }
                else // ItemID = ItemID
                {
                    if (inventoryItem[index1].Data.MaxStack == 0) // Single Item 
                    {
                        SwapItems(index1, index2);
                    }
                    else
                    {
                        int total = inventoryItem[index1].Quantity + inventoryItem[index2].Quantity;
                        if (total > inventoryItem[index1].Data.MaxStack)
                        {
                            inventoryItem[index2].Quantity = inventoryItem[index2].Data.MaxStack;
                            inventoryItem[index1].Quantity = (total - inventoryItem[index2].Data.MaxStack);
                        }
                        else
                        {
                            inventoryItem[index1].Data = null;
                            inventoryItem[index2].Quantity = total;
                        }
                    }
                }
            }
            else
            {
                SwapItems(index1, index2);
            }
        }
       OnInventoryChange2Slot?.Invoke(this, index1, index2);
    }

    
    public bool SwitchEquipItem(int index1, EEquipType t)
    {
        if (inventoryItem[index1].Data == null) return false;

        Equip ne = EquipManager.Instance.Equipment((Equip)inventoryItem[index1].Data, t);
        if ( ne != null)
        {
            inventoryItem[index1].Data = ne;
        }
        else
        {
            inventoryItem[index1].Data = null;
        }
        OnInventoryChangeSlot?.Invoke(this, index1);
        return true;
    }

    void SwapItems(int index1, int index2)
    {
        InventoryItem i = inventoryItem[index1];
        inventoryItem[index1] = inventoryItem[index2];
        inventoryItem[index2] = i;
    }
}
