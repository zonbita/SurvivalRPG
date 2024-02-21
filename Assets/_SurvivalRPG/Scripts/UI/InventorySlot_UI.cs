using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot_UI : MonoBehaviour, IDropHandler
{
    public System.Action<int, Image, int> OnSlotData;
    [SerializeField]Image imageItem;
    [SerializeField]TMP_Text Quantitytext;
    [SerializeField]DragItem_UI dragItem;
    InventoryManager manager;
    int slot = -1;


    public void Init(InventoryManager manager, int slot, Sprite imageItem, int quantity)
    {
        Set(manager, slot, imageItem, quantity);
    }

    public void Set(InventoryManager manager, int slot, Sprite imageItem, int quantity)
    {
        this.manager = manager;
        this.slot = slot;
        SetImage(imageItem);
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
    }

    public void OnDrop(PointerEventData eventData)
    {

        GameObject go = eventData.pointerDrag;
        InventorySlot_UI inv = go.GetComponent<InventorySlot_UI>();
    }
}
