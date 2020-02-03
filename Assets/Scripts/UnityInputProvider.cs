using UnityEngine;

public class UnityInputProvider : IInputProvider 
{
    public bool GetDash()
    {
        return Input.GetButton("Dash");
    }

    public bool GetJump()
    {
        return Input.GetButton("Jump");
    }

    public bool GetCrouch()
    {
        return Input.GetButton("Crouch");
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }
}