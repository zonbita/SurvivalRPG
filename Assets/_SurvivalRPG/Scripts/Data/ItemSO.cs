using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EItemRarity { Common, Uncommon, Rare, Epic, Legendary, Gold }
public enum EItemCategory { Armor, Weapon, Resource, Gold, Gem, Material, Repice, Enchantment, Consumable }
public enum EItemID { Null, PickAxe, Hammer, Axe, Gold }
public enum EEquipType { Helmet, Gloves, Chest, Pants, Boots, Weapon, Food, Potion }
public enum EPlayerAction { PickUP, Chop, Dig, Mine1, Mine2, Mine3 }
public abstract class ItemSO : ScriptableObject
{
    public Sprite Icon;
    public EItemID ItemID = EItemID.Null;
    public string Name;
    public EItemRarity Rarity;
    [TextArea] public string Description;
    public GameObject Prefab;

    public int Quantity = 0;
    public int MaxStack;
    public int Duration = 100;
    public List<string> Tags;

    [Header("Attribute")]
    public EPlayerAction playerAction;
    public EItemCategory Category;
    public EEquipType EquipType;
    public List<Attribute> Attributes;

    public abstract void Use();

    private void Awake()
    {
        if (Prefab)
            PrefabUtility.InstantiatePrefab(Prefab as GameObject);
    }

}
