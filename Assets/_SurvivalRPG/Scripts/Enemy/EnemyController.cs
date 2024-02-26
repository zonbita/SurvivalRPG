using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterCombat))]

public class EnemyController : CharacterBase
{
    [SerializeField] internal List<Attribute> attributes;
    
    CharacterCombat characterCombat;

    Coroutine coroutineNormal;
    Vector3 Target;
    void Start()
    {
        characterCombat = GetComponent<CharacterCombat>();
        characterCombat.OnNormal += OnNormal;
    }

    IEnumerator StartNormal()
    {
        characterCombat.Normal();
        yield return new WaitForSeconds(4);
        StopCoroutine(StartNormal());
        coroutineNormal = null;
    }

    private void OnNormal()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 6;
        navMesh.SetDestination(transform.position + randomDirection);
    }

    void Update()
    {
        if (characterCombat.stats.currentHealth <= 0)
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
                characterCombat.Battle();

                // Check Player Health
                if (GameManager.Instance.Player.GetComponent<PlayerStats>().isDead == false)
                {
                      
                    characterCombat.Attack(GameManager.Instance.Player.GetComponent<PlayerStats>());
                    FaceTarget();
                }
                else
                {
                    // Stop battle animations
                    characterCombat.Normal();
                    target = null;
                }
            }
        }
        else
        {
            if (coroutineNormal == null)
                coroutineNormal = StartCoroutine(StartNormal());
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, -90, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
