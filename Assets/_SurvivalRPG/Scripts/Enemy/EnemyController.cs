using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attribute))]
[RequireComponent (typeof(Movement))]
public class EnemyController : CharacterBase
{
    Attribute attribute;
    Movement movement;
    NavMeshAgent nav;
    Vector3 Target;

    void Start()
    {
        attribute = GetComponent<Attribute>();
        movement = GetComponent<Movement>();
    }


    void Update()
    {
        
    }
}
