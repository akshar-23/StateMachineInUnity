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
        Vector2 movementInput = Variables.movementAction.ReadValue<Vector2>();

        Variables.verticalVelocity += Variables.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, Variables.verticalVelocity * Time.deltaTime, movementInput.y).normalized * Variables.sprintSpeed * Time.deltaTime;

        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        if (Variables.sprintAction.ReadValue<float>() <= 0)
        {
            SwitchState(player, player.walkState);
        }

        // switch the state to jump state if jump key is pressed
        if (Variables.jumpAction.triggered && Variables.controller.isGrounded)
        {
            SwitchState(player, player.jumpState);
        }

        // switch to the slide state if crouch key is pressed while sprinting
        if (Variables.crouchAction.triggered && Variables.controller.isGrounded)
        {
            SwitchState(player, player.slideState);
        }

        // switch to fall state
        if (!(Variables.jumpAction.triggered) && !Variables.controller.isGrounded)
        {
            SwitchState(player, player.fallState);
        }
    }

    public override void SwitchState(StateManager player, BaseState newState)
    {
        player.currentState.ExitState(player);
        player.currentState = newState;
        player.currentState.EnterState(player);
    }
}
