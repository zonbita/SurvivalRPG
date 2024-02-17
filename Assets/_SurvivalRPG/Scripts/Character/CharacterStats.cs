using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Health
{
    public virtual void Awake()
    {
        
    }
    protected override void Start()
    {

    }

    protected override void Dead()
    {
        base.Dead();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
