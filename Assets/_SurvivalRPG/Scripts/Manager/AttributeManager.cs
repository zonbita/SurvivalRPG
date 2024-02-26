using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
public enum EAttribute
{
    Strength, Intelligence, Fortitude, Willpower, Vitality, Armor,
    ArmorPenetration, LifeDrain, ArmorPiercing, BlockChance, MovementSpeed, CooldownReduction,
    CriticalHitChance, CriticalHitDamage, CriticalHitResistance
};

public class AttributeManager : Singleton<AttributeManager>
{

    [SerializeField] TMP_Text PlayerAttributeText;
    public static Action OnInit;

    AttributeTotal EquipmentAttributeTotal = new();

    private void Awake()
    {
        OnInit += Init;
    }
    public void Init()
    {
        
    }

    /// <summary>
    /// Update Player All Attribute
    /// </summary>
    public void UpdateEquipAttribute()
    {
        EquipmentAttributeTotal = CalAttributeTotal(EquipManager.Instance.GetEquipList);
        if(EquipmentAttributeTotal != null) { Character_Player.Instance.playerStats.UpdateAttributeTotal(EquipmentAttributeTotal); }

        /*        if(EquipmentAttributeTotal != null )
                {
                    foreach (var a in EquipmentAttributeTotal.attributes)
                    {
                        print(a.Key.ToString() + " " + a.Value.ToString());
                    }
                }*/

        if (Character_Player.Instance.playerStats != null) UpdatePlayerAttributeUI();
    }


    void UpdatePlayerAttributeUI()
    {

        string format = "";
        foreach (Attribute att in Character_Player.Instance.playerStats.attributes)
        {
            float total = EquipmentAttributeTotal != null ? EquipmentAttributeTotal.Get_A_AttributeTotal(att) : 0;
            format += string.Format("<color={0}>{1}</color>: <color=white>{2}</color> + ({3})\n", 
                GlobalVar.GetAttributeColor(att.Name), 
                att.Name.ToString(), 
                att.Value,
                total
                );
        }
        PlayerAttributeText.text = format;
    }
    
    public AttributeTotal CalAttributeTotal(Item[] itemList)
    {
        if(itemList.Length > 0)
        {
            AttributeTotal AT = new();

            foreach (Item item in itemList)
            {
                foreach (Attribute a in item.Attributes)
                {
                    AT.Add(a);
                }
            }
            return AT;
        }

        return null;
    }
 
}
