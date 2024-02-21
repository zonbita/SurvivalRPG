using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveByKey))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(SurvivalManager))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(AlwaysLookAt))]
[RequireComponent (typeof(Looter))]
public class Character_Player : CharacterBase
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    MoveByKey movement;
    PlayerStats playerStats;
    SurvivalManager survivalManager;
    PlayerInventory playerInventory;
    AlwaysLookAt alwaysLookAt;
    Looter looter;

    CinemachineVirtualCamera virtualCamera;
    protected override void Awake()
    {
        base.Awake();
        
        movement = GetComponent<MoveByKey>();
        playerStats = GetComponent<PlayerStats>();
        survivalManager = GetComponent<SurvivalManager>();
        looter = GetComponent<Looter>();
        playerInventory = GetComponent<PlayerInventory>();
        alwaysLookAt = GetComponent<AlwaysLookAt>();
        playerStats.OnDied += OnDied;
        playerStats.OnHealth.AddListener(OnHealth);
    }
    void Start()
    {
        virtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = this.transform;
       
        playerInventory.InitInventory(25, null);

        GameManager.Instance.GameRevive += () => 
        {
            _animator.Play("Idle");
            movement.enabled = true;
            looter.enabled = true;
            survivalManager.enabled = true;
            alwaysLookAt.enabled = false;
            playerStats.enabled = true;
        };
    }

    private void OnDied()
    {
        movement.enabled = false;
        looter.enabled = false;
        survivalManager.enabled = false;
        GameManager.Instance.GameOver();
    }
    
    public override void Attack()
    {
        /*        if (Time.time - attackTime < 0.5)
                    return;

                attackTime = Time.time;*/
        _animator.SetTrigger(AttackHash);
    }

    private void OnHealth(float hp)
    {
       
    }

    


}
