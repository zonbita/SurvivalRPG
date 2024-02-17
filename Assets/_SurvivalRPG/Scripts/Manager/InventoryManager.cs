using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{

    public ItemSO[] inventoryItem;
    
    public ItemSO[] InventoryItem => inventoryItem;
    public int inventorySize => InventoryItem.Length;

    bool isFull = false;

    // Event
    public System.Action<int, ItemSO> OnInventoryChangeSlot;

    public void InitInventory(int size, ItemSO[] item)
    {

        inventoryItem = new ItemSO[size];

        if (item == null) return;

        if (item.Length == 0) return;

        for (int i = 0; i < item.Length - 1; i++)
        {
            if (item[i] != null && inventoryItem[i])
            inventoryItem[i] = item[i];
        }
    }

    public bool CheckFull()
    {
        foreach (ItemSO item in inventoryItem)
        {
            if (item.Quantity < item.MaxStack) return false;
        }
        return true;
    }

    public int GetEmptySlot()
    {

        for (int i = 0; i < inventoryItem.Length - 1; i++)
        {
            if (inventoryItem[i] == null) return i;

            if (inventoryItem[i].ItemID == EItemID.Null) return i;
        }
        return -1;
    }

    public void AddMore(ItemSO item)
    {
        int quantity = item.Quantity;

        for (int i = 0; i < inventoryItem.Length - 1; i++)
        {
            if (inventoryItem[i].ItemID == item.ItemID)
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

    public void Add(ItemSO item)
    {
        if (item.ItemID == EItemID.Null) return;

        int index = GetEmptySlot();

        if (index != -1)
        {
            inventoryItem[index] = item;
        }
    }
}
