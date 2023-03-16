using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState(StateManager player)
    {
        player.transform.localScale = Vector3.one * 0.5f;
        Debug.Log("Crouch State Entered!");
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

        if (Variables.crouchAction.ReadValue<float>() <= 0)
        {
            player.transform.localScale = Vector3.one;

            // switch to idle state
            if (movementInput.magnitude <= 0f && Variables.controller.isGrounded)
            {
                SwitchState(player, player.idleState);
            }

            // switch to walk state
            if (Variables.movementAction.ReadValue<Vector2>().magnitude > 0 && Variables.controller.isGrounded)
            {
                SwitchState(player, player.walkState);
            }

            // switch to fall state
            if (!(Variables.jumpAction.triggered) && !Variables.controller.isGrounded)
            {
                SwitchState(player, player.fallState);
            }
        }
    }

    public override void SwitchState(StateManager player, BaseState newState)
    {
        player.currentState.ExitState(player);
        player.currentState = newState;
        player.currentState.EnterState(player);
    }
}
