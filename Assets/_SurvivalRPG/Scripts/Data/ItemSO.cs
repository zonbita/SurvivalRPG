using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ECategoryItem { Tool, Weapon, Resource, Gold }
public enum EItemID { Null, PickAxe, Hammer, Axe, Gold }
public enum EEquipType { Helmet, Armor, Chest, Pants, Boost, Weapon, Food, Potion } 

public abstract class ItemSO : ScriptableObject
{
    public Sprite Icon;
    public EItemID ItemID = EItemID.Null;
    public string Name;
    [TextArea] public string Description;
    public GameObject Prefab;

    public int Quantity = 0;
    public int MaxStack;
    public List<string> Tags;

    [Header("Attribute")]
    public ECategoryItem Category;
    public EEquipType EquipType;
    public List<Attribute> Attributes;

    public abstract void Use();

    private void Awake()
    {
        if (Prefab)
            PrefabUtility.InstantiatePrefab(Prefab as GameObject);
    }

}
