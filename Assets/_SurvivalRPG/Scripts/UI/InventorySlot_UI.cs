using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot_UI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] Image imageItem;
    [SerializeField] TMP_Text Quantitytext;
    [SerializeField] DragItem_UI dragItem;
    [SerializeField] public InventoryManager inv;
    [HideInInspector] public int slot = -1;
    public Action<int> OnClick;

    public void Init(InventoryManager inv, int slot)
    {
        Set(inv, slot);
    }

    public void Set(InventoryManager inv, int slot)
    {
        this.inv = inv;
        this.slot = slot;
        if(inv.inventoryItem[slot].Data != null )
        {
            SetImage(inv.inventoryItem[slot].Data.Icon);
            SetQuantity(inv.inventoryItem[slot].Quantity);
        }
        else
        {
            SetImage(null);
            SetQuantity(0);
        }
    }

    public void SetNew()
    {

        if (inv.inventoryItem[slot].Data != null)
        {
            SetImage(inv.inventoryItem[slot].Data.Icon);
            SetQuantity(inv.inventoryItem[slot].Quantity);
        }
        else
        {
            SetImage(null);
            SetQuantity(0);
        }
    }

    public void SetEmpty(InventoryManager inv)
    {
        this.inv = inv;
        slot = -1;
        Quantitytext.enabled = false;
        imageItem.enabled = false; 
    }


    void SetImage(Sprite imageItem)
    {
        if(imageItem != null)
        {
            this.imageItem.enabled = true;
            this.imageItem.overrideSprite = imageItem;
        }
        else
        {
            this.imageItem.enabled = false;
        }
       
    }

    void SetQuantity(int quantity)
    {
        Quantitytext.enabled = (quantity < 1 ? false : true);
        Quantitytext.text = quantity + "";
    }

    public void OnDrop(PointerEventData eventData)
    {

        GameObject go = eventData.pointerDrag;
        DragItem_UI drag = go.GetComponent<DragItem_UI>();

        if (drag.SlotUI.inv == null || inv == null) return;

        if (drag.SlotUI.inv != inv) // Inventory is not the same
        {
            
        }
        else
        {
            inv.SwitchItem(drag.SlotUI.slot, slot);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(slot != -1) OnClick(slot);
    }
}
