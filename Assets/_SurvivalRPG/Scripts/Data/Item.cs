using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public override void Equip()
    {
    }

    public void Pickup(ItemDrop item)
    {
        
    }
}
