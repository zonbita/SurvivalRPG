using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class InventoryEquipSlot_UI : MonoBehaviour, IDropHandler
{
    public System.Action<int, Image, int> OnSlotData;
    [SerializeField] Sprite[] imageList;
    [SerializeField] Image imageItem;
    [SerializeField] TMP_Text Quantitytext;
    [SerializeField] DragItem_UI dragItem;
    public EEquipType equipType;

    int slot = -1;

    private void Awake()
    {
        if(imageList[(int)equipType] != null)
        imageItem.sprite = imageList[(int)equipType];
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
        DragItem_UI d = go.GetComponent<DragItem_UI>();
        d.RootTransform = transform;
        dragItem.RootTransform = d.RootTransform;


    }
}
