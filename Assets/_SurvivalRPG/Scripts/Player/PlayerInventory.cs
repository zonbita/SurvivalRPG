using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryManager
{
    public static PlayerInventory Instance { get; private set; }
    public int SelectItem {  get; private set; }

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
            //GameManager.Instance.EquipBtn.interactable = inventoryItem[slot].Data = true ? true : false;
            GameManager.Instance.DropBtn.interactable = inventoryItem[slot].Data.isDrop = true ? true : false;
            //GameManager.Instance.DropBtn.interactable = inventoryItem[slot].Data.isDrop = true ? true : false;

        }

    }
}
