using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprintState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Sprint State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();

        verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.sprintSpeed * Time.deltaTime;
        
        player.controller.Move(player.transform.TransformDirection(movement));

        if (player.controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }

        if (player.sprintAction.ReadValue<float>() <= 0)
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.jumpAction.triggered && player.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to the slide state if crouch key is pressed while sprinting
        if (player.crouchAction.triggered && player.controller.isGrounded)
        {
            player.SwitchState(player.slideState);
        }

        // switch to fall state
        if (!(player.jumpAction.triggered) && !player.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
