using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Consumable", menuName = "Item/NewConsumable")]
public class Consumable : Item
{
    public int healthGain;
    public int hungerGain;
    public int thirstyGain;


    public override void Use()
    {
        // Heal the player
        PlayerStats playerStats = Character_Player.Instance.playerStats;

        if (healthGain > 0)
            playerStats.Heal(healthGain);

        if (hungerGain > 0)
            playerStats.HealHunger(hungerGain);

        if (thirstyGain > 0)
            playerStats.HealThirsty(thirstyGain);

        Debug.Log(name + " consumed!");

        RemoveFromInventory();
    }

}
