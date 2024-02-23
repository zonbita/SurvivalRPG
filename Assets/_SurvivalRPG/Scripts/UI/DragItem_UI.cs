using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem_UI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Image image;
    [HideInInspector] public Transform RootTransform;
    Vector3 position;
    public InventorySlot_UI SlotUI;

    private void Awake()
    {
        image = GetComponent<Image>();
        SlotUI = GetComponentInParent<InventorySlot_UI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
