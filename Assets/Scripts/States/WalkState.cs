using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Walk State Entered!");
        player.var.sprintAction.Enable();
    }

    public override void UpdateState(StateManager player)
    {
        Vector2 movementInput = player.var.movementAction.ReadValue<Vector2>();
        
        player.var.verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, player.var.verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.moveSpeed * Time.deltaTime;


        player.var.controller.Move(player.transform.TransformDirection(movement));

        if (player.var.controller.isGrounded && player.var.verticalVelocity < 0f)
        {
            player.var.verticalVelocity = 0f;
        }


        // switch the state to idle state if wasd movement not detected
        if (movementInput.magnitude <= 0f && player.var.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
            player.var.sprintAction.Disable();
        }

        // switch to sprint state
        if(player.var.sprintAction.ReadValue<float>() > 0 && player.var.controller.isGrounded)
        {
            player.SwitchState(player.sprintState);
        }

        // switch to crouch state
        if (player.var.crouchAction.ReadValue<float>() > 0 && player.var.controller.isGrounded)
        {
            player.SwitchState(player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.var.jumpAction.triggered && player.var.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to fall state
        if (!(player.var.jumpAction.triggered) && !player.var.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }

    }
}
