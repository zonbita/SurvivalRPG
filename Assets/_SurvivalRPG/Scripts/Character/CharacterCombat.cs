using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{

    public float attackRate = 1f;
    private float attackCountdown = 0f;

    public event System.Action OnAttack;
    public event System.Action OnBattle;
    public event System.Action OnNormal;
    public event System.Action OnDeath;

    internal CharacterStats stats;
    CharacterStats enemyStats;

    public Slider healthSlider;

    void Start()
    {
        stats = GetComponent<CharacterStats>();


        healthSlider.maxValue = stats.maxHealth;
        healthSlider.value = stats.currentHealth;
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
        if (OnDeath != null)
            OnDeath();
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        enemyStats.TakeDamage(10);
    }
}
