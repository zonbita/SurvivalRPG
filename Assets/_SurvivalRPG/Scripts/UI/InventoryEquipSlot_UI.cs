using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]

public class InventoryEquipSlot_UI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public System.Action<int, Image, int> OnSlotData;

    [SerializeField] Image imageItem;
    [SerializeField] TMP_Text Quantitytext;
    [SerializeField] DragItem_UI dragItem;
    public EEquipType equipType;

    int slot = -1;

    public Action<EEquipType> OnRightClickEvent;
    internal void AssignAction()
    {

    }


    private void Awake()
    {
        if(EquipManager.Instance.imageList[(int)equipType] != null)
        imageItem.sprite = EquipManager.Instance.imageList[(int)equipType];
    }

    public void Init(int slot, int quantity)
    {
        Set(slot, quantity);
    }

    public void Set(int slot, int quantity)
    {
        this.slot = slot;
        SetQuantity(quantity);
    }

    public void SetEmpty()
    {
        slot = -1;
        Quantitytext.enabled = false;
        imageItem.enabled = false;

    }

    void SetQuantity(int quantity)
    {
        Quantitytext.enabled = (quantity < 1 ? false : true);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject go = eventData.pointerDrag;
        DragItem_UI drag = go.GetComponent<DragItem_UI>();


        if (drag.SlotUI.inv == null) return;

        bool b = drag.SlotUI.inv.SwitchEquipItem(drag.SlotUI.slot, equipType);
        if (b)
        {
            OnRightClickEvent?.Invoke(equipType);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRightClickEvent?.Invoke(equipType);
    }
}
