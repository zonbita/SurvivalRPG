using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtons_UI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] EClickPlayerInventoryButton ActionName;
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerInventory.Instance?.ClickButton(ActionName);
    }
}
