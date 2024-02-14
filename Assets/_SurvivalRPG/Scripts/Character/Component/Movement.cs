using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public Vector3 moveInput;
    public bool IsMoving { get; private set; }
    public bool IsRunning;
    [HideInInspector] public Rigidbody rb;

    [Space][Header("Walking & Running")][SerializeField]
    protected float _walkSpeed = 3;

    private Animator _animator;
    Controls controls;
    Vector2 _vector2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new Controls();
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        _vector2 = controls.Player.Move.ReadValue<Vector2>();
        moveInput.x = _vector2.x;
        moveInput.z = _vector2.y;

        if (_vector2 != Vector2.zero && transform.position != moveInput)
        {
            rb.MovePosition(transform.position + moveInput * _walkSpeed * Time.fixedDeltaTime);
            
            // Rotate direction
            Quaternion lookRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        _animator.SetFloat("speed", _vector2.magnitude);
    }
}
