using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem_UI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    [HideInInspector] public Transform RootTransform;
    Vector3 position;
    InventorySlot_UI inventorySlot_UI;
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventorySlot_UI = GetComponentInParent<InventorySlot_UI>();

        RootTransform = transform.parent;
        position = this.transform.position;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(RootTransform);
        transform.position = position;
        eventData.Reset();
        image.raycastTarget = true;
    }

}
