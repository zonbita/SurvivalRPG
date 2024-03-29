﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterBase : MonoBehaviour
{
    public Rigidbody rb;
    public FillBar healthBar;
    public Animator _animator; 
    public TextMeshProUGUI nameTMP;
    internal NavMeshAgent navMesh;

    [Header("Tầm phát hiện")]
    public float detectRadius = 10;

    public Transform target;

    protected bool bFreeze;
    protected float attackTime;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponent<FillBar>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void RegisterGameEvent()
    {
        GameManager.Instance.GameOver += GameOver;
        GameManager.Instance.GamePause += GamePause;
        GameManager.Instance.GameRevive += GameRevive;
        GameManager.Instance.GamePlay += GamePlay;
        GameManager.Instance.GameStart += GameStart;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameOver -= GameOver;
        GameManager.Instance.GamePause -= GamePause;
        GameManager.Instance.GameRevive -= GameRevive;
        GameManager.Instance.GamePlay -= GamePlay;
        GameManager.Instance.GameStart -= GameStart;
    }

    public virtual void OnDestroy()
    {
        GameManager.Instance.GameOver -= GameOver;
        GameManager.Instance.GamePause -= GamePause;
        GameManager.Instance.GameRevive -= GameRevive;
        GameManager.Instance.GamePlay -= GamePlay;
        GameManager.Instance.GameStart -= GameStart;
    }

    private void Start()
    {
        RegisterGameEvent();
    }

    public virtual void GamePlay()
    {

    }

    public virtual void GameStart()
    {

    }

    public virtual void GamePause()
    {
 
    }

    public virtual void GameRevive()
    {

    }

    public virtual void GameOver()
    {

    }

    public virtual void DisableComponent()
    {
        rb.isKinematic = true;
    }

    public virtual void Death()
    {

    }

    public virtual void Attack()
    {
        if (bFreeze)
            return;

        if (Time.time - attackTime < 0)
            return;
    }

}

public enum AIState
{
    IDLE,
    CHASING,
    ATTACKING,
    DEATH,
    FREEZE
}