using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public Sprite[] imageList;
    public Dictionary<EEquipType, Equip> slots = new Dictionary<EEquipType, Equip>();
    
    internal List<Attribute> EquipAttributes;
    [SerializeField] List<InventoryEquipSlot_UI> InventoryEquipSlot_UIs;

    public System.Action<Equip> OnChangeEquipSlot;

    private void Awake()
    {
        foreach(EEquipType type in Enum.GetValues(typeof(EEquipType)))
        {
            slots.Add(type, null); // Add EquipSlots

            foreach(InventoryEquipSlot_UI slot in InventoryEquipSlot_UIs)
            {
                if (slot.equipType == type) {
                    slot.OnRightClickEvent += OnRightClickEvent;
                } 
            }
        }
    }

    private void OnRightClickEvent(EEquipType type)
    {
        print(type);
    }

    internal Equip Equipment(Equip item, EEquipType t)
    {
        foreach (KeyValuePair<EEquipType, Equip> k in slots)
        {
            if(k.Key == t )
            {
                if (k.Value != null)
                {
                    Equip e = k.Value;
                    slots[t] = item;
                    return e;
                }
                else
                {
                    slots[t] = item;
                    return null;
                }

            }
        }
        return null;
    }
}
