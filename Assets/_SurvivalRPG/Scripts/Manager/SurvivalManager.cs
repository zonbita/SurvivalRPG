using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;

public class SurvivalManager : Singleton<SurvivalManager>
{
    [Header("[Hunger]")]
    public float hungerDelay = 60f;
    public float hungerDamage = 1;
    Coroutine hungerCoroutine;

    [Header("[Thirsty]")]
    public float thirstyDelay = 90f;
    public float thirstyDamage = 1;
    Coroutine thirstyCoroutine;

    [Header("[Health]")]
    public float healthDelay = 4f;
    public float healthDamage = -0.01f;
    Coroutine healthCoroutine;

    enum EHungerLevel { Level0, Level1, Level2, Level3 };
    EHungerLevel hungerLevel = EHungerLevel.Level0;
    PlayerStats PlayerStats;

    void Start()
    {
        PlayerStats = GetComponent<PlayerStats>();

        hungerCoroutine = StartCoroutine(DoHungerDamage(hungerDelay));
        thirstyCoroutine = StartCoroutine(DoThirstyDamage(thirstyDelay));
    
    }

    void Update()
    {
        // Hunger
        if (PlayerStats.currentHunger > 50 && hungerLevel != EHungerLevel.Level0)
        {
            hungerLevel = EHungerLevel.Level0;
        }
        else if (PlayerStats.currentHunger <= 50 && PlayerStats.currentHunger > 25 && hungerLevel != EHungerLevel.Level1)
        {
            hungerLevel = EHungerLevel.Level1;

            string[] messages = { "I'm extremely hungry", "My stomach grumbled violently", "I'm starving" };


            Notice_Board.Instance.ShowNotice(messages[Random.Range(0, messages.Length - 1)], 3);


        }
        else if (PlayerStats.currentHunger <= 25 && PlayerStats.currentHunger > 0 && hungerLevel != EHungerLevel.Level2)
        {
            hungerLevel = EHungerLevel.Level2;

            Notice_Board.Instance.ShowNotice("I'm dying of starvation", 3);


        }
        else if (PlayerStats.currentHunger <= 0 && hungerLevel != EHungerLevel.Level3)
        {
            hungerLevel = EHungerLevel.Level3;
            if (healthCoroutine == null)
                healthCoroutine = StartCoroutine(DoHealthDamage(healthDelay));
        }


        // Thirsty 
        if (PlayerStats.currentThirsty <= 50 && PlayerStats.currentThirsty > 25)
        {

            string[] messages = { "I want to drink something", "I really need to drink" };

            Notice_Board.Instance.ShowNotice(messages[Random.Range(0, messages.Length - 1)], 3);
        }
        else if (PlayerStats.currentThirsty <= 25 && PlayerStats.currentThirsty > 0)
        {

            string messages =  "I'm dying of dehydration" ;

            Notice_Board.Instance.ShowNotice(messages, 3);

        }
        else if (PlayerStats.currentThirsty <= 0)
        {
            if(healthCoroutine == null)
                healthCoroutine = StartCoroutine(DoHealthDamage(healthDelay));
        }
    }

    IEnumerator DoHungerDamage(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            PlayerStats.SurvivalDamage(hungerDamage, EPlayerStats.Hunger);
        }
    }

    IEnumerator DoThirstyDamage(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            PlayerStats.SurvivalDamage(thirstyDamage, EPlayerStats.Thirsty);
        }
    }

    IEnumerator DoHealthDamage(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            PlayerStats.SurvivalDamage(healthDamage, EPlayerStats.Health);
        }
    }
}
