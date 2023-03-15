using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprintState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Sprint State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        Vector2 movementInput = player.var.movementAction.ReadValue<Vector2>();

        player.var.verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, player.var.verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.sprintSpeed * Time.deltaTime;
        
        player.var.controller.Move(player.transform.TransformDirection(movement));

        if (player.var.controller.isGrounded && player.var.verticalVelocity < 0f)
        {
            player.var.verticalVelocity = 0f;
        }

        if (player.var.sprintAction.ReadValue<float>() <= 0)
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.var.jumpAction.triggered && player.var.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to the slide state if crouch key is pressed while sprinting
        if (player.var.crouchAction.triggered && player.var.controller.isGrounded)
        {
            player.SwitchState(player.slideState);
        }

        // switch to fall state
        if (!(player.var.jumpAction.triggered) && !player.var.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
