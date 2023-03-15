using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public abstract class BaseState
{
    protected float moveSpeed = 5f;
    protected float sprintSpeed = 10f;
    protected float gravity = -9.81f;
    protected float verticalVelocity;
    protected float jumpHeight = 1.5f;




    public virtual void EnterState(StateManager player)
    {

    }

    public virtual void UpdateState(StateManager player)
    {
        
    }
}
