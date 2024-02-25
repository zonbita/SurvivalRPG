using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryManager
{
    public static PlayerInventory Instance { get; private set; }
    public int SelectSlot = -1;


    private void Awake()
    {
        Instance = this;
    }

    public override void InitInventory(int size, ItemSO[] item, int quantity)
    {
        base.InitInventory(size, item, quantity);
        GenerateUI();
        OnInventoryChangeSlot += OnChange;
        OnInventoryChange2Slot += OnChange2Slot;
    }
    private void OnChange(InventoryManager manager, int index)
    {

        if (GameManager.Instance.InventoryHud == null) return;

        InventorySlot_UI ui = GameManager.Instance.InventoryHud.transform.GetChild(index).GetComponent<InventorySlot_UI>();
        ui.SetNew();
    }

    private void OnChange2Slot(InventoryManager manager, int index1, int index2)
    {
        if (GameManager.Instance.InventoryHud == null) return;

        InventorySlot_UI ui = GameManager.Instance.InventoryHud.transform.GetChild(index1).GetComponent<InventorySlot_UI>();
        ui.SetNew();

        ui = GameManager.Instance.InventoryHud.transform.GetChild(index2).GetComponent<InventorySlot_UI>();
        ui.SetNew();
    }


    void GenerateUI()
    {
        if (GameManager.Instance.InventorySlotUI == null) return;

        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlot_UI ui = Instantiate(GameManager.Instance.InventorySlotUI, GameManager.Instance.InventoryHud.transform).GetComponent<InventorySlot_UI>();
            ui.Init(this, i);
            ui.OnClick += OnClick;
        }

    }

    private void OnClick(int slot)
    {
        if (inventoryItem[slot].Data == null)
        {
            GameManager.Instance.DropBtn.interactable = false;
            GameManager.Instance.EquipBtn.interactable = false;
            GameManager.Instance.UseBtn.interactable = false;
        }
        else
        {
            SelectSlot = slot;
            GameManager.Instance.EquipBtn.interactable = isEquipItem(SelectSlot) ? true : false;
            GameManager.Instance.DropBtn.interactable = inventoryItem[slot].Data.isDrop == true ? true : false;
            GameManager.Instance.UseBtn.interactable = inventoryItem[slot].Data.Category == EItemCategory.Consumable ? true : false;

        }

    }

    // Buttons in Player Inventory
    public void ClickButton(EClickPlayerInventoryButton button)
    {
        if (inventoryItem[SelectSlot].Data != null)
        {
            switch (button)
            {
                case EClickPlayerInventoryButton.Use:
                    inventoryItem[SelectSlot].Data.ConsumableFromInventory += ConsumableFromInventory;
                    inventoryItem[SelectSlot].Data.Use();
                    break;
                case EClickPlayerInventoryButton.Equip:

                    inventoryItem[SelectSlot].Data.Use();
                    break;
                case EClickPlayerInventoryButton.Drop:
                    inventoryItem[SelectSlot].Data.RemoveFromInventory += RemoveFromInventory;
                    SpawnItemManager.Instance.SpawnAItem(inventoryItem[SelectSlot].Data, inventoryItem[SelectSlot].Quantity, this.transform.position + new Vector3(0,0,1));
                    inventoryItem[SelectSlot].Data.RemoveFromInventory();
                    break;
                default:
                    break;
            }
        }
    }

    private void RemoveFromInventory()
    {
        if (inventoryItem[SelectSlot].Data != null)
        {
            inventoryItem[SelectSlot].Empty();
            OnInventoryChangeSlot(this, SelectSlot);
        }
    }

    private void ConsumableFromInventory()
    {
        if(inventoryItem[SelectSlot].Data.MaxStack > 0)
        {
            inventoryItem[SelectSlot].Quantity -= 1;
            if (inventoryItem[SelectSlot].Quantity == 0) inventoryItem[SelectSlot].Empty();
        }
        else
        {
            inventoryItem[SelectSlot].Empty();
        }
        OnInventoryChangeSlot(this, SelectSlot);
    }
}
public enum EClickPlayerInventoryButton { Use, Equip, Drop }