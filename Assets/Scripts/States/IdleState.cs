using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public override void EnterState()
    {
        Debug.Log("Idle State Entered!");
    }

    public override void UpdateState()
    {
        Variables.verticalVelocity += Variables.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(0, Variables.verticalVelocity * Time.deltaTime, 0).normalized * Time.deltaTime;
        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        player.currentState.SwitchState();
    }

    public override void SwitchState()
    {
        // switch the state to walking state if wasd movement detected
        if (Variables.movementAction.ReadValue<Vector2>().magnitude > 0 && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.walkState;
            player.currentState.EnterState();
        }

        // switch the state to crouch state if crouch key is pressed
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
