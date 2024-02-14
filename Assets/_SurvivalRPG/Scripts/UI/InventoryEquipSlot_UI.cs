using UnityEngine.UI;
using UnityEngine;
using System;

public class InventoryEquipSlot_UI : MonoBehaviour
{
    Image[] images;
    EEquipType[] eEquipTypes;
    ItemSO[] itemSOs;

    private void Awake()
    {
        Init(9);
    }

    private void Init(int size)
    {
        for(int i = 0; i < size; i++)
        {

        }
    }
}
