using UnityEngine;

public class MoveByJoyStick : MoveByKey
{
    [SerializeField]
    private JoyStick _joyStick;

    protected override Vector2 GetInput()
    {
        return _joyStick.Input;
    }
}
