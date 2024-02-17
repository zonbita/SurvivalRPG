using UnityEngine;

public class MoveByKey : Movement
{
    Controls controls;

    protected override void Awake()
    {
        base.Awake();
        controls = new Controls();
    }
    // Update is called once per frame
    protected override void Update()
    {

        //GetComponent<Character_Player>()?.Attack();

        _vector2 = GetInput();
        moveInput.x = _vector2.x;
        moveInput.z = _vector2.y;

        if (_vector2 != Vector2.zero && transform.position != moveInput)
        {
            rb.MovePosition(transform.position + moveInput * _walkSpeed * Time.deltaTime);


        }
        _animator.SetFloat("speed", _vector2.magnitude);
    }

    private void LateUpdate()
    {
        // Rotate direction
        if(moveInput != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
        }

    }

    protected override Vector2 GetInput()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }


    protected override void OnEnable()
    {
        controls.Enable();
    }

    protected override void OnDisable()
    {
        controls.Disable();
    }

}
