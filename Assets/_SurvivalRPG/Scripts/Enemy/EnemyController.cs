using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attribute))]
[RequireComponent (typeof(Movement))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    Attribute attribute;
    Movement movement;
    NavMeshAgent nav;
    Vector3 Target;

    void Start()
    {
        attribute = GetComponent<Attribute>();
        movement = GetComponent<Movement>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
    }
}
