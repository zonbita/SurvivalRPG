using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attribute))]
[RequireComponent (typeof(Movement))]
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : CharacterBase
{
    Attribute attribute;
    Movement movement;
    Vector3 Target;
    CharacterCombat characterCombat;
    CharacterStats characterStats;
    void Start()
    {
        attribute = GetComponent<Attribute>();
        movement = GetComponent<Movement>();
        characterCombat = GetComponent<CharacterCombat>();
        characterStats = GetComponent<CharacterStats>();
    }


    void Update()
    {
        if (characterStats.currentHealth <= 0)
            return;

        target = GameManager.Instance.Player.transform;

        if (target == null)
            return;


        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= detectRadius)
        {

            navMesh.SetDestination(target.position);
            if (distance <= navMesh.stoppingDistance)
            {
                // In Battle
                GetComponent<CharacterCombat>().Battle();

                // Check Player Health
                if (GameManager.Instance.Player.GetComponent<PlayerStats>().isDead == false)
                {
                      
                    characterCombat.Attack(GameManager.Instance.Player.GetComponent<PlayerStats>());
                    FaceTarget();
                }
                else
                {
                    // Stop battle animations
                    GetComponent<CharacterCombat>().Normal();
                    characterCombat.healthSlider.gameObject.SetActive(false);
                    target = null;
                }
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
