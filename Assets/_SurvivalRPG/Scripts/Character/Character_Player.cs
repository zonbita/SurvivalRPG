using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Character_Player : CharacterBase
{
    Movement movement;

    private void OnValidate()
    {
        movement = GetComponent<Movement>();
    }
    private void Awake()
    {

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
