
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Item/NewItem")]
public class Item : ItemSO, IPickup
{
    private void Awake()
    {
            
    }

    public override void Use()
    {
        
    }
    public void Pickup(ItemDrop item)
    {
        
    }

    public override void EquipSlot(int slot)
    {

    }
}
