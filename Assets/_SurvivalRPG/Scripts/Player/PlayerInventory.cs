using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryManager
{
    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


}
