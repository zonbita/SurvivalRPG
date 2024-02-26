using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
    public float attackRate = 1f;
    public float damage = 1f;
    private float attackCountdown = 0f;

    public event System.Action OnAttack;
    public event System.Action OnBattle;
    public event System.Action OnNormal;
    public event System.Action OnDeath;

    internal CharacterStats stats;
    CharacterStats enemyStats;

    AnimationsController animationsController;

    public Slider healthSlider;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
        animationsController = GetComponentInChildren<AnimationsController>();

        healthSlider.maxValue = stats.maxHealth;
        healthSlider.value = stats.currentHealth;
        stats.OnDied += Death;
        stats.OnTakeDamage += onTakeDamage;
    }

    private void onTakeDamage()
    {
        animationsController.Hit();

    }

    void Update()
    {
        if (stats.currentHealth <= 0)
            return;

        healthSlider.value = stats.currentHealth;
        attackCountdown -= Time.deltaTime;
    }

    public void Attack(CharacterStats enemyStats)
    {
        if (attackCountdown <= 0f)
        {
            this.enemyStats = enemyStats;
            attackCountdown = 1f / attackRate;
            animationsController.Attack();
            StartCoroutine(DoDamage(enemyStats, .6f));

            if (OnAttack != null)
            {
                OnAttack();
            }
        }
    }

    public void Battle()
    {
        if (OnBattle != null)
            OnBattle();
    }

    public void Normal()
    {
        if (OnNormal != null)
            OnNormal();
    }

    public void Death()
    {
        healthSlider.gameObject.SetActive(false);
        transform.AddComponent<TimeLifeDisable>().LifeTime = 3;

        if (OnDeath != null)
            OnDeath();
    }


    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        enemyStats.TakeDamage(damage);
    }
}
