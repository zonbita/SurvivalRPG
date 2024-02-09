using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private readonly int DieHash = Animator.StringToHash("Dead");

    [SerializeField] private float MaxHealth;
    [SerializeField] private float currentHealth;

    [HideInInspector][SerializeField] private Animator _animator;

    public Action OnDied;
    public UnityEvent<float> OnHealth;

    private void OnValidate() => _animator = GetComponent<Animator>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        enabled = true;
        currentHealth = MaxHealth;
        OnHealth?.Invoke(currentHealth / MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        CalculatorTakeDamage(damage);
       
        if (currentHealth <= 0)
        {
            Dead();
            HandleKillZombieEarnMoney();
        }
    }

    private void CalculatorTakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHealth?.Invoke(currentHealth / MaxHealth);
    }

    void HandleKillZombieEarnMoney()
    {
    }

    void Dead()
    {
        if (_animator != null)
            _animator.SetTrigger(DieHash);

        currentHealth = 0;
        OnDied?.Invoke();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > MaxHealth)
            currentHealth = MaxHealth;

        OnHealth?.Invoke(currentHealth / MaxHealth);
    }
}
