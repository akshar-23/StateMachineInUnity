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

    public InputAction movementAction;

    public virtual void EnterState(StateManager player)
    {
        player.controls = new InputControls();
        player.controls.Enable();
        movementAction = player.controls.Player.Movement;
    }

    public virtual void UpdateState(StateManager player)
    {
        verticalVelocity += gravity * Time.deltaTime;

        // move the character based on the current vertical velocity
        Vector3 movement = Vector3.up * verticalVelocity * Time.deltaTime;
        player.controller.Move(movement);

        if (player.controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }
    }
}
