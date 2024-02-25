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
    [SerializeField] Image imageBG;
    [SerializeField] TMP_Text Quantitytext;
    [SerializeField] DragItem_UI dragItem;
    public EEquipType equipType;

    int slot = -1;

    public Action<EEquipType> OnRightClickEvent;

    public Action<Equip> OnChangeEquipSlot;

    private void Awake()
    {
        if (EquipManager.Instance.imageList[(int)equipType] != null)
            imageBG.sprite = EquipManager.Instance.imageList[(int)equipType];
    }

    public void OnChangeEquip(Equip equip)
    {
        SetImage(equip.Icon);
        SetQuantity(0);
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

    void SetImage(Sprite imageItem)
    {
        if (imageItem != null)
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject go = eventData.pointerDrag;
        DragItem_UI drag = go.GetComponent<DragItem_UI>();


        if (drag.SlotUI.inv == null) return;

        bool b = drag.SlotUI.inv.SwitchEquipItem(drag.SlotUI.slot, equipType);
        if (b)
        {

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRightClickEvent?.Invoke(equipType);
    }
}
