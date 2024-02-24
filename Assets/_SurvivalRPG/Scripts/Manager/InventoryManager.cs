using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool isFull => inventoryItem.Where(x => x.Data == null).Any() == false;

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
        if (item.ItemID == EItemID.Null || isFull) return;

        
        if (item.MaxStack > 0)
        {
            while (quantity > 0 && !isFull)
            {
                for (int i = 0; i < inventoryItem.Length - 1; i++)
                {
                    if (inventoryItem[i].Data.ItemID == item.ItemID)
                    {
                        int total = quantity + inventoryItem[i].Quantity;
                        if (total > item.MaxStack)
                        {
                            inventoryItem[i].Quantity = item.MaxStack;
                            quantity = total - item.MaxStack;
                        }
                        else
                            inventoryItem[i].Quantity += quantity;
                    }
                }
            }
        }
        else
        {
            int index = GetEmptySlot();
            inventoryItem[index].Data = item;
            inventoryItem[index].Quantity = inventoryItem[index].Data.Category == EItemCategory.Armor ||
                                            inventoryItem[index].Data.Category == EItemCategory.Weapon ||
                                            inventoryItem[index].Data.Category == EItemCategory.Tool ||
                                            inventoryItem[index].Data.Category == EItemCategory.Repice
                                            ? 0 : quantity;

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

        EquipManager.Instance.OnChangeEquipSlot((Equip)inventoryItem[index1].Data);
        Equip EQ = EquipManager.Instance.Equipment((Equip)inventoryItem[index1].Data, t);
        if (EQ != null)
        {
            inventoryItem[index1].Data = EQ;
            
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

    public int GetQuantityTotal(EItemID EItemID)
    {
        return inventoryItem.Sum(x => x.Data.ItemID == EItemID ? x.Quantity : 0);
    }
}
