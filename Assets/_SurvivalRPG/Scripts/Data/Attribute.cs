using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttribute { Strengh, Intelligence, Fortitude, Willpower, Vitality, Armor, 
    ArmorPenetration, LifeDrain, ArmorPiercing, BlockChance, MovementSpeed, CooldownReduction,
    CriticalHitChance, CriticalHitDamage, CriticalHitResistance
};

[System.Serializable]
public class Attribute
{
    public EAttribute attribute;
    public float Value;

}
