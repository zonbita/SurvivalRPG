using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
public enum EAttribute
{
    Strengh, Intelligence, Fortitude, Willpower, Vitality, Armor,
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
            format += string.Format("<color={0}>{1}</color>: <color=white>{2}</color>\n", GetAttributeColor(att.attribute), att.attribute.ToString(), att.Value);
        }
        PlayerAttributeText.text = format;
    }

    public string GetAttributeColor(EAttribute e)
    {
        string s = "white";
        switch (e)
        {
            case EAttribute.Strengh:
                s = "red";
                break;
            case EAttribute.Intelligence:
                s = "#87CEFA";
                break;
            case EAttribute.Fortitude:
                s = "green";
                break;
            case EAttribute.Willpower:
                s = "purple";
                break;
            case EAttribute.Vitality:
                s = "yellow";
                break;
            case EAttribute.Armor:
                s = "#FFA07A";
                break;
            case EAttribute.ArmorPenetration:
                s = "orange";
                break;
            case EAttribute.LifeDrain:
                s = "#778899";
                break;
            case EAttribute.ArmorPiercing:
                s = "#483D8B";
                break;
            case EAttribute.BlockChance:
                s = "#FFFACD";
                break;
            case EAttribute.MovementSpeed:
                s = "#F5F5DC";
                break;
            case EAttribute.CooldownReduction:
                s = "#FF69B4";
                break;
            case EAttribute.CriticalHitChance:
                s = "#A9A9A9";
                break;
            case EAttribute.CriticalHitDamage:
                s = "#5F9EA0";
                break;
            case EAttribute.CriticalHitResistance:
                s = "#A52A2A";
                break;
        }
        return s;
    }
}
