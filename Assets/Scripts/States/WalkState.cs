using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState()
    {
        Debug.Log("Walk State Entered!");
        Variables.sprintAction.Enable();
    }

    public override void UpdateState()
    {
        Variables.verticalVelocity += Variables.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(Variables.movementAction.ReadValue<Vector2>().x, Variables.verticalVelocity * Time.deltaTime, Variables.movementAction.ReadValue<Vector2>().y).normalized * Variables.moveSpeed * Time.deltaTime;


        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        player.currentState.SwitchState();
    }

    public override void SwitchState()
    {
        // switch the state to idle state if wasd movement not detected
        if (Variables.movementAction.ReadValue<Vector2>().magnitude <= 0f && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.idleState;
            player.currentState.EnterState();
            Variables.sprintAction.Disable();
        }

        // switch to sprint state
        if (Variables.sprintAction.ReadValue<float>() > 0 && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.sprintState;
            player.currentState.EnterState();
        }

        // switch to crouch state
        if (Variables.crouchAction.ReadValue<float>() > 0 && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.crouchState;
            player.currentState.EnterState();
        }

        // switch the state to jump state if jump key is pressed
        if (Variables.jumpAction.triggered && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.jumpState;
            player.currentState.EnterState();
        }

        // switch to fall state
        if (!(Variables.jumpAction.triggered) && !Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.fallState;
            player.currentState.EnterState();
        }
    }
}
