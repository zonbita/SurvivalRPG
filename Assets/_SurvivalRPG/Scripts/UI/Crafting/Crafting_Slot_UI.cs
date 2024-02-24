using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Crafting_Slot_UI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image imageItem;
    [SerializeField] TMP_Text Quantitytext;
    [SerializeField] FillBar fillBar;
    internal int slot = -1;
    public Action<int> OnClick;
    
    public void Init(int slot, Sprite sprite)
    {
        this.slot = slot;
        imageItem.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick(slot);
    }
}
