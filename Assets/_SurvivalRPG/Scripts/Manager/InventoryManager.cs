using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public ItemSO Data;
    public int Quantity;
    public int Duration;

    public void Empty()
    {
        Data = null;
        Quantity = 0;
        Duration = 0;
    }
}

[System.Serializable]
public class InventoryManager : MonoBehaviour
{

    public InventoryItem[] inventoryItem;
    public InventoryItem[] InventoryItem => inventoryItem;
    public int inventorySize => InventoryItem.Length;

    private bool isFull => inventoryItem.Where(x => x.Data == null).Any() == false;

    internal bool isEquipItem(int slot) 
    {
        EItemCategory category = inventoryItem[slot].Data.Category;
        if (category == EItemCategory.Armor || category == EItemCategory.Weapon ) return true;
        return false;
    } 

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

        int Max = item.MaxStack;
        
        if (Max > 0)
        {
            while (quantity > 0 && !isFull)
            {
                int[] Is = inventoryItem
                    .Select((item, index) => new { Item = item, Index = index })
                    .Where(itemWithIndex => itemWithIndex.Item.Data != null && itemWithIndex.Item.Data.ItemID == item.ItemID && itemWithIndex.Item.Quantity < Max) 
                    .Select(itemWithIndex => itemWithIndex.Index)
                    .ToArray();

                if (Is.Length > 0)
                {
                    foreach (int i in Is)
                    {
                        if (quantity == 0 || isFull) break;
                        else
                        {
                            int t = quantity + inventoryItem[i].Quantity;
                            if (t > Max)
                            {
                                inventoryItem[i].Quantity = Max;
                                quantity = t - quantity;
                            }
                            else 
                            {
                                inventoryItem[i].Quantity = t;
                                quantity = 0;
                            } 
                            OnInventoryChangeSlot?.Invoke(this, i);
                        }
                    }

                    if (quantity == 0 || isFull) break;
                    else
                    {
                        int s = GetEmptySlot();
                        if (s != -1)
                        {
                            inventoryItem[s].Data = item;
                            inventoryItem[s].Quantity += quantity;
                            OnInventoryChangeSlot?.Invoke(this, s);
                            quantity = 0;
                        }
                    }
                }
                else
                {
                    int s = GetEmptySlot();
                    if (s != -1)
                    {
                        inventoryItem[s].Data = item;
                        inventoryItem[s].Quantity += quantity;
                        OnInventoryChangeSlot?.Invoke(this, s);
                        quantity = 0;
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
                    int MaxStack = inventoryItem[index1].Data.MaxStack;
                    if (inventoryItem[index1].Data.MaxStack == 0 || inventoryItem[index1].Quantity == MaxStack || inventoryItem[index2].Quantity == MaxStack) // Single Item 
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
        if (inventoryItem[index1].Data == null || !(inventoryItem[index1].Data as Equip)) return false;


        bool b = EquipManager.Instance.Equipment((Equip)inventoryItem[index1].Data, out Equip outitem);
        if (b)
        {
            if (outitem)
            {
                inventoryItem[index1].Data = outitem;
                OnInventoryChangeSlot?.Invoke(this, index1);
            }
            else
            {
                inventoryItem[index1].Data = null;
                OnInventoryChangeSlot?.Invoke(this, index1);
            }

        }
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


    public void DropItem(int slot)
    {
        if(inventoryItem[slot].Data != null) return;

    }
}
