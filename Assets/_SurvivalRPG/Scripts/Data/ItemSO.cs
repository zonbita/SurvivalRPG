using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum EItemRarity { Common, Uncommon, Rare, Epic, Legendary, Gold }
public enum EItemCategory { Armor, Weapon, Tool, Resource, Gold, Gem, Material, Repice, Enchantment, Consumable }
public enum EItemID { Null, Stick, Wood, Rock, PickAxe, Hammer, Axe, Gold, Golden_Helmet, Golden_Chestplate, Golden_Leggings, Golden_Boots }
public enum EPlayerAction { PickUP, Chop, Dig, Mine1, Mine2, Mine3 }
public enum EEquipType { Helmet, Chest, Gloves, Pants, Boots, Jewels, Gems, Weapon, Food, Potion }
public abstract class ItemSO : ScriptableObject
{
    public Sprite Icon;
    public EItemID ItemID = EItemID.Null;
    public string Name;
    public EItemRarity Rarity;
    [TextArea] public string Description;
    public GameObject Prefab;


    public int MaxStack;
    public int Duration = 100;
    public List<string> Tags;

    [Header("Attribute")]
    public EPlayerAction playerAction;
    public EItemCategory Category;
    public List<Attribute> Attributes;

    public abstract void Use();

}
