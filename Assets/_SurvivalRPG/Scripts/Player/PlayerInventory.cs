using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryManager
{
    public static PlayerInventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public override void InitInventory(int size, ItemSO[] item)
    {
        base.InitInventory(size, item);
        GenerateUI();
        OnInventoryChangeSlot += OnChange;
    }

    private void OnChange(InventoryManager manager, int index, ItemSO sO)
    {
        
        if (GameManager.Instance.InventoryHud == null) return;
        
        InventorySlot_UI ui = GameManager.Instance.InventoryHud.transform.GetChild(index).GetComponent<InventorySlot_UI>();
        ui.Set(this, index, inventoryItem[index].Icon, inventoryItem[index].Quantity);
    }

    void GenerateUI()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if (GameManager.Instance.InventorySlotUI == null) return;

            InventorySlot_UI ui = Instantiate(GameManager.Instance.InventorySlotUI, GameManager.Instance.InventoryHud.transform).GetComponent<InventorySlot_UI>();
            if(inventoryItem[i] != null)
            ui.Set(this, i, inventoryItem[i].Icon, inventoryItem[i].Quantity);
            else ui.SetEmpty();
        }

    }
}
