using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "Item/NewEquip")]
public class Equip : Item
{
    public EEquipType EquipType;

    public override void Use()
    {
        //EquipManager.Instance.Equipment(this, EquipType);  // Equip
    }
    
}

