using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    internal readonly int DieHash = Animator.StringToHash("isDead");

    [SerializeField] internal float maxHealth = 100;
    [SerializeField] internal float currentHealth = 100;

    [SerializeField] internal Animator _animator;

    public Action OnDied, OnTakeDamage;
    public UnityEvent<float> OnHealth;

    internal bool isDead = false;
    public virtual void OnValidate() => _animator = GetComponentInChildren<Animator>();

    protected virtual void Start()
    {
        Init();

    }

    protected virtual void Init()
    {
        enabled = true;
        currentHealth = maxHealth;
        //OnHealth?.Invoke(currentHealth / MaxHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        CalculatorTakeDamage(damage);

        if (currentHealth <= 0)
        {
            if (!isDead)
                Dead();
        }
        else OnTakeDamage?.Invoke();
    }

    void CalculatorTakeDamage(float damage)
    {
        currentHealth = Math.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealth?.Invoke(currentHealth / maxHealth);
    }

    protected virtual void Dead()
    {
        isDead = true;

        currentHealth = 0;

        if (_animator != null)   _animator.SetTrigger(DieHash);

        OnDied?.Invoke();
    }

    public virtual void Heal(float amount)
    {
        currentHealth = Math.Clamp(currentHealth + amount, 0, maxHealth);

        OnHealth?.Invoke(currentHealth / maxHealth);
    }

    public void TakeDamageEffect(float damage, float max)
    {
        
    }
}
