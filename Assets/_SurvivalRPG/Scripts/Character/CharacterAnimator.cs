using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    public Animator animator;

    NavMeshAgent navmeshAgent;
    CharacterCombat combat;

    protected virtual void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        combat.OnAttack += OnAttack;
        combat.OnBattle += OnBattle;
        combat.OnNormal += OnNormal;
        combat.OnDeath += OnDeath;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Speed", navmeshAgent.velocity.magnitude / navmeshAgent.speed, .1f, Time.deltaTime);
    }

    protected virtual void OnAttack()
    {
        animator.SetBool("isAttack", true);
        animator.SetBool("isBattle", false);
    }

    protected virtual void OnBattle()
    {
        animator.SetBool("isBattle", true);
    }

    protected virtual void OnNormal()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isBattle", false);
    }

    protected virtual void OnDeath()
    {
        animator.SetBool("isDead", true);
    }
}
