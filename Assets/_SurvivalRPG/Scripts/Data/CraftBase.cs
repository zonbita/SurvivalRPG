using System.Collections.Generic;
using UnityEngine;

public enum ECraftType { Player, Table }

[System.Serializable]
public struct RequiredItems
{
    public ItemSO Data;
    public int Quantity;
}

public abstract class CraftBase: ScriptableObject
{
    public ItemSO Data;
    public int Quantity;
    public List<RequiredItems> RequiredItems;
    public ECraftType eCraftType;
}
