using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterBase : MonoBehaviour
{
    public Action<GameObject> Action_Die;
    public Rigidbody rb;
    public FillBar healthBar;
    public Animator animator;
    public TextMeshProUGUI nameTMP;
    private NavMeshAgent navMesh;

    [Header("Tầm phát hiện")]
    public float detectRadius;

    [Header("Tốc độ di chuyển")]
    public float speed;
    protected bool bFreeze;
    protected float attackTime;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponent<FillBar>();
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
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
        Action_Die(this.gameObject);
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