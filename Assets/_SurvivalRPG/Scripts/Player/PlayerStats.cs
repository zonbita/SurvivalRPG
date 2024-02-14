using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] FillBar hungerFillBar, thirstyFillBar, healthFillBar;
    [SerializeField] List<Attribute> attributes;
    public enum EPlayerStats { Health, Hunger, Thirsty }

    public float maxHunger = 100f,maxThirsty = 100f;
    public float currentHunger;
    public float currentThirsty;
    GameObject canvas;
    public override void Awake()
    {
        canvas = GameObject.Find("Canvas");
        healthFillBar = canvas.transform.Find("PlayerStats").Find("HP").GetComponentInChildren<FillBar>();
        hungerFillBar = canvas.transform.Find("PlayerStats").Find("Food").GetComponentInChildren<FillBar>();
        thirstyFillBar = canvas.transform.Find("PlayerStats").Find("Water").GetComponentInChildren<FillBar>();
    }
    protected override void Start()
    {
        base.Start();

        GameManager.Instance.GameRevive += () =>
        {
            currentHealth = maxHealth;
            currentHunger = maxHunger;
            currentThirsty = maxThirsty;
        };
    }

    public void SurvivalDamage(float damage, EPlayerStats type)
    {
        if (type == EPlayerStats.Hunger)
        {
            currentHunger = Mathf.Clamp(currentHunger - damage, 0, maxHunger);

            if (currentHunger <= 0)
            {

            }
            UpdateUI(EPlayerStats.Hunger);
        }

        if (type == EPlayerStats.Thirsty)
        {
            currentThirsty = Mathf.Clamp(currentThirsty - damage, 0, maxThirsty);
            if (currentThirsty <= 0)
            {

            }
            UpdateUI(EPlayerStats.Thirsty);
        }

        if (type == EPlayerStats.Health)
        {
            TakeDamage(damage);
            UpdateUI(EPlayerStats.Health);
        }
    }


    public void HealHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void HealThirsty(float amount)
    {
        currentThirsty += amount;
        currentThirsty = Mathf.Clamp(currentThirsty, 0, maxThirsty);
    }

    public void UpdateUI(EPlayerStats name)
    {
        switch (name)
        {
            case EPlayerStats.Hunger:
                hungerFillBar.SetPercent(currentHunger, maxHunger);
                break;
            case EPlayerStats.Thirsty:
                thirstyFillBar.SetPercent(currentThirsty, maxThirsty);
                break;
            case EPlayerStats.Health:
                healthFillBar.SetPercent(currentHealth, maxHealth);
                break;
        }
    }
}
