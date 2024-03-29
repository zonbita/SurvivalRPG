using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] internal List<Attribute> attributes;
    [HideInInspector] public AttributeTotal attTotal = new();
    public enum EPlayerStats { Health, Hunger, Thirsty }

    public float maxHunger = 100f,maxThirsty = 100f;
    public float currentHunger;
    public float currentThirsty;


    public override void Awake()
    {
        base.Awake();
        GameManager.Instance.GameRevive += GameRevive;


    }

    private void GameRevive()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirsty = maxThirsty;
        isDead = false;


        GetHUD();
    }

    public void UpdateAttributeTotal(AttributeTotal EquipAttributeTotal)
    {
        attTotal.Clear();
        attTotal.AddList(attributes);
        attTotal.Add_A_AttributeTotal(EquipAttributeTotal);
    }

    private void GetHUD()
    {
        UpdateUI(EPlayerStats.Health);
        UpdateUI(EPlayerStats.Hunger);
        UpdateUI(EPlayerStats.Thirsty);
    }

    protected override void Start()
    {
        base.Start();
        attTotal.AddList(attributes);
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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateUI(EPlayerStats.Health);
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

    protected override void Dead()
    {
        base.Dead();
    }

    public void UpdateUI(EPlayerStats name)
    {
        switch (name)
        {
            case EPlayerStats.Hunger:
                GameManager.Instance.hungerFillBar?.SetPercent(currentHunger, maxHunger);
                break;
            case EPlayerStats.Thirsty:
                GameManager.Instance.thirstyFillBar?.SetPercent(currentThirsty, maxThirsty);
                break;
            case EPlayerStats.Health:
                GameManager.Instance.healthFillBar?.SetPercent(currentHealth, maxHealth);
                break;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.GameRevive += GameRevive;
    }
}
