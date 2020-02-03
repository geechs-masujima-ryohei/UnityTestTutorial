using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProvider
{
    bool GetDash();
    bool GetJump();
    bool GetCrouch();
    Vector3 GetMoveDirection();
}
