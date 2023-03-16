using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{

    public override void EnterState(StateManager player)
    {
        Debug.Log("Idle State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        Variables.verticalVelocity += Variables.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(0, Variables.verticalVelocity * Time.deltaTime, 0).normalized * Time.deltaTime;
        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        // switch the state to walking state if wasd movement detected
        if (Variables.movementAction.ReadValue<Vector2>().magnitude > 0 && Variables.controller.isGrounded)
        {
            SwitchState(player, player.walkState);
        }

        // switch the state to crouch state if crouch key is pressed
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
