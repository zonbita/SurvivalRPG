using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(SurvivalManager))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent (typeof(Looter))]
public class Character_Player : CharacterBase
{
    Movement movement;
    PlayerStats playerStats;
    SurvivalManager survivalManager;
    PlayerInventory playerInventory;
    Looter looter;

    CinemachineVirtualCamera virtualCamera;

 
    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerStats = GetComponent<PlayerStats>();
        survivalManager = GetComponent<SurvivalManager>();
        looter = GetComponent<Looter>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStats.OnDied += OnDied;
        playerStats.OnHealth.AddListener(OnHealth);
    }
    void Start()
    {
        virtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = this.transform;

        playerInventory.InitInventory(10, null);

        GameManager.Instance.GameRevive += () => 
        {
            movement.enabled = true;
            looter.enabled = true;
            survivalManager.enabled = true;
        };
    }

    void Update()
    {

    }

    private void OnDied()
    {
        movement.enabled = false;
        looter.enabled = false;
        survivalManager.enabled = false;
        GameManager.Instance.GameOver();
    }

    private void OnHealth(float hp)
    {
       
    }

 


}
