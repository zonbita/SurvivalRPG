using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public Vector3 moveInput;
    public bool IsMoving { get; private set; }
    public bool IsRunning => _running;
    [HideInInspector] public Rigidbody rb;

    [Space][Header("Walking & Running")][SerializeField]
    protected float _walkSpeed = 3;
    protected bool _running;

    Controls controls;
    Vector2 _vector2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        _vector2 = controls.Player.Move.ReadValue<Vector2>();
        moveInput.x = _vector2.x;
        moveInput.z = _vector2.y;
        rb.MovePosition(transform.position + moveInput * _walkSpeed * Time.fixedDeltaTime);
    }
}
