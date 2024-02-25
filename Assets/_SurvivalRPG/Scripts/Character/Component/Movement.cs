using UnityEditor.ShaderGraph;
using UnityEngine;



public class Movement : MonoBehaviour
{
    internal Vector3 moveInput;
    internal bool IsMoving { get; private set; }
    internal bool IsRunning;
    [HideInInspector] protected Rigidbody rb;

    [Space][Header("Walking & Running")][SerializeField]
    protected float _walkSpeed = 3;

    protected Animator _animator;

    internal Vector2 _vector2;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {

        _animator.SetFloat("speed", _vector2.magnitude);
    }

    protected virtual Vector2 GetInput() { return Vector2.zero; }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }
}
