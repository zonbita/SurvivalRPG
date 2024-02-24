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
    private void Awake()
    {
        OnInit += Init;
    }
    public void Init()
    {
        
    }

    public void UpdatePlayerAttribute()
    {
        if(Character_Player.Instance.playerStats == null) return;

        UpdatePlayerAttributeUI();
    }

    void UpdatePlayerAttributeUI()
    {
        string format = "";
        foreach (Attribute att in Character_Player.Instance.playerStats.attributes)
        {
            format += string.Format("<color={0}>{1}</color>: <color=white>{2}</color>\n", GlobalVar.GetAttributeColor(att.attribute), att.attribute.ToString(), att.Value);
        }
        PlayerAttributeText.text = format;
    }

 
}
