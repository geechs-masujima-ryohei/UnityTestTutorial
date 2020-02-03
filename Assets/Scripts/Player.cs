using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PersonInfo Info { get; private set; }

    private IInputProvider inputProvider;

    [SerializeField]
    private new Rigidbody rigidbody = default;

    public Vector3 Velocity => rigidbody.velocity;

    [SerializeField]
    private GroundChecker groundChecker = default;

    public bool IsGround => groundChecker.IsGround;
    public bool IsCrouch { get; private set; }

    private void Awake()
    {
    }

    public void Initialize(PersonInfo personInfoInfo, IInputProvider input)
    {
        Info = personInfoInfo;

        inputProvider = input;

        name += $"[{Info.Name}]";
    }

    private void ActivateAI(IInputProvider input)
    {
    }

    protected void Update()
    {
        Jump(inputProvider.GetJump());

        Vector3 inputVector = inputProvider.GetMoveDirection();
        bool isDash = inputProvider.GetDash();

        //移動
        Move(inputVector, isDash);

        Crouch(inputProvider.GetCrouch());
    }


    private int jumpCoolTime;

    public void Jump(bool isJump)
    {
        if (isJump)
        {
            if (groundChecker.IsGround)
            {
                if (jumpCoolTime <= 0)
                {
                    rigidbody.AddForce(Vector3.up * 300);
                    jumpCoolTime = 10;
                }
            }
        }

        if (jumpCoolTime > 0)
        {
            jumpCoolTime--;
        }
    }

    public void Move(Vector3 direction, bool isDash)
    {
        Vector3 vector;
        float dashFix = isDash ? 0.3f : 0.1f;
        vector = transform.position + direction * dashFix;

        rigidbody.MovePosition(vector);
    }

    private bool isCrouchFixPosition;

    public void Crouch(bool isCrouch)
    {
        if (isCrouch)
        {
            Debug.Log("Crouching");
            if (isCrouchFixPosition)
            {
                transform.position -= new Vector3(0f, 0.25f, 0f);
                isCrouchFixPosition = false;
            }
        }
        else
        {
            if (!isCrouchFixPosition)
            {
                transform.position += new Vector3(0f, 0.25f, 0f);
                isCrouchFixPosition = true;
            }
        }

        transform.localScale = new Vector3(1f, isCrouch ? 0.5f : 1f, 1f);

        IsCrouch = isCrouch;
    }
}