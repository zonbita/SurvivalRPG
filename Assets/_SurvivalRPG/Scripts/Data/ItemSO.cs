using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum EItemRarity { Common, Uncommon, Rare, Epic, Legendary, Gold }
public enum EItemCategory { Armor, Weapon, Tool, Resource, Gold, Gem, Material, Repice, Enchantment, Consumable }
public enum EItemID { Null, Stone, Stick, Wood, Copper_Ore, Gold_Ore, Iron_Ore, Stone_PickAxe, 
    Stone_Hammer, Stone_Axe, Gold, Golden_Helmet, Golden_Chestplate, Golden_Leggings, Golden_Boots }
public enum EPlayerAction { PickUP, Chop, Dig, Mine1, Mine2, Mine3 }
public enum EEquipType { Helmet, Chest, Gloves, Pants, Boots, Jewels, Book, Weapon, Food, Potion }

[CreateAssetMenu]
public abstract class ItemSO : ScriptableObject
{
    [field:SerializeField] public Sprite Icon;
    public EItemID ItemID = EItemID.Null;
    public string Name;
    public EItemRarity Rarity;
    [TextArea] public string Description;
    public GameObject Prefab;
    public bool isDrop = true;
    public int MaxStack;
    public int Duration = 100;
    public List<string> Tags;

    [Header("----------Attribute---------------")]
    public EPlayerAction Action;
    public EItemCategory Category;
    public List<Attribute> Attributes;

    [Header("----------blueprints---------------")]
    public Blueprint Blueprint;
    public abstract void Use();

}


public enum ECraftType { Player, Table }

[System.Serializable]
public class RequiredItem
{
    public ItemSO item;
    [Min(1)]public int Quantity = 1;

}

[System.Serializable]
public class Blueprint
{
    public List<RequiredItem> RequiredItems;
    public ECraftType ECraftType;
    [Min(1)] public int Quantity = 1;
    public float CraftingTime = 2;
}
