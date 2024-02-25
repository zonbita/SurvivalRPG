using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "Item/NewEquip")]
public class Equip : Item
{
    public EEquipType EquipType;


    public override void EquipSlot(int slot)
    {
        PlayerInventory.Instance.SwitchEquipItem(slot, EquipType);
    }
}

