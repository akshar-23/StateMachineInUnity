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
        Variables.sprintAction.Enable();
    }

    public override void UpdateState(StateManager player)
    {
        Vector2 movementInput = Variables.movementAction.ReadValue<Vector2>();

        Variables.verticalVelocity += Variables.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, Variables.verticalVelocity * Time.deltaTime, movementInput.y).normalized * Variables.moveSpeed * Time.deltaTime;


        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }


        // switch the state to idle state if wasd movement not detected
        if (movementInput.magnitude <= 0f && Variables.controller.isGrounded)
        {
            SwitchState(player, player.idleState);
            Variables.sprintAction.Disable();
        }

        // switch to sprint state
        if (Variables.sprintAction.ReadValue<float>() > 0 && Variables.controller.isGrounded)
        {
            SwitchState(player, player.sprintState);
        }

        // switch to crouch state
        if (Variables.crouchAction.ReadValue<float>() > 0 && Variables.controller.isGrounded)
        {
            SwitchState(player, player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (Variables.jumpAction.triggered && Variables.controller.isGrounded)
        {
            SwitchState(player, player.jumpState);
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
