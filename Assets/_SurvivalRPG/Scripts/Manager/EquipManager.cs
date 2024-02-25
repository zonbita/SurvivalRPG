using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public struct EquipContainer
{
    public EEquipType EquipType;
    public Equip Data;
}

public class EquipManager : Singleton<EquipManager>
{
    public Sprite[] imageList;
    [SerializeField] List<InventoryEquipSlot_UI> InventoryEquipSlot_UIs;

    internal List<Attribute> EquipAttributes;

    [SerializeField] private EquipContainer[] equipmentSlots = new EquipContainer[10];

    public EquipContainer[] EquipmentSlots
    {
        get
        {
            return equipmentSlots;
        }
    }

    /// <summary>
    /// Get Equipment Item List
    /// </summary>
    /// <returns></returns>
    public Equip[] GetEquipList
    {
        get
        {
            if(equipmentSlots.Length > 0)
            {
                List<Equip> equipList = new List<Equip>();

                foreach (EquipContainer equipContainer in equipmentSlots)
                {
                    if (equipContainer.Data != null)
                    {
                        equipList.Add(equipContainer.Data);
                    }
                }

                return equipList.ToArray();
            }
            else return null;
        }
    }

    private void Awake()
    {
        foreach(EEquipType type in Enum.GetValues(typeof(EEquipType)))
        {
            foreach(InventoryEquipSlot_UI slot in InventoryEquipSlot_UIs)
            {
                if (slot.equipType == type) {
                    slot.OnRightClickEvent += OnRightClickEvent;
                } 
            }
        }

        for(int i = 0; i < 10; i++)
        {
            EquipContainer equipContainer = new EquipContainer
            {
                EquipType = (EEquipType)i,
                Data = null
            };

            // Add 
            equipmentSlots[i] = equipContainer;
        }
    }

    private void OnRightClickEvent(EEquipType type)
    {
        print(type);

    }

    internal bool Equipment(Equip item, out Equip outitem)
    {
        for (int i = 0; i < 10; i++)
        {
            if (EquipmentSlots[i].EquipType == item.EquipType )
            {
                
                if(EquipmentSlots[i].Data != null)
                {
                    outitem = EquipmentSlots[i].Data;
                    EquipmentSlots[i].Data = item;
                }
                else
                {
                    outitem = null;
                    EquipmentSlots[i].Data = item;
                }

                
                AttributeManager.Instance.UpdatePlayerAttribute();
                Character_Player.Instance.Attach(item.EquipType, item.Prefab);
                InventoryEquipSlot_UIs[i].OnChangeEquip(item);
                //AttributeManager.Instance.CalAttributeTotal(new Item[] { equipmentSlots[i].Data });

                return true;
            }
        }

        outitem = null;
        return false;
    }
}
