using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Walk State Entered!");
        player.sprintAction.Enable();
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();
        
        verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.moveSpeed * Time.deltaTime;


        player.controller.Move(player.transform.TransformDirection(movement));

        if (player.controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }


        // switch the state to idle state if wasd movement not detected
        if (movementInput.magnitude <= 0f && player.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
            player.sprintAction.Disable();
        }

        // switch to sprint state
        if(player.sprintAction.ReadValue<float>() > 0 && player.controller.isGrounded)
        {
            player.SwitchState(player.sprintState);
        }

        // switch to crouch state
        if (player.crouchAction.ReadValue<float>() > 0 && player.controller.isGrounded)
        {
            player.SwitchState(player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.jumpAction.triggered && player.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to fall state
        if (!(player.jumpAction.triggered) && !player.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }

    }
}
