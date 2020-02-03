using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AIInputProvider : IInputProvider
{
    private readonly Player selfPlayer;
    private readonly Player targetPlayer;

    public AIInputProvider(Player selfPlayer, Player targetPlayer)
    {
        this.selfPlayer = selfPlayer;
        this.targetPlayer = targetPlayer;
    }

    public bool GetDash()
    {
        return false;
    }

    public bool GetJump()
    {
        return !targetPlayer.IsGround;
    }

    public bool GetCrouch()
    {
        return targetPlayer.IsCrouch;
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(targetPlayer.transform.position.x - selfPlayer.transform.position.x, 0, 0).normalized * 0.5f;
    }
}