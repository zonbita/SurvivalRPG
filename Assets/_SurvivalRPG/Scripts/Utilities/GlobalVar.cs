using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class GlobalVar
{
    public static string GetAttributeColor(EAttribute e)
    {
        string s = "white";
        switch (e)
        {
            case EAttribute.Strength:
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

    public static string GetRarityColor(EItemRarity i)
    {
        string s = "white";
        switch (i)
        {
            case EItemRarity.Gold:
                s = "yellow";
                break;
            case EItemRarity.Uncommon:
                s = "white";
                break;
            case EItemRarity.Common:
                s = "aqua";
                break;
            case EItemRarity.Rare:
                s = "Orange";
                break;
            case EItemRarity.Epic:
                s = "purple";
                break;
            case EItemRarity.Legendary:
                s = "red";
                break;
        }
        return s;
    }
}
