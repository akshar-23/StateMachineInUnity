using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    public override void EnterState()
    {
        Debug.Log("Fall State Entered!");

        Variables.movementDirection = new Vector3(Variables.movementAction.ReadValue<Vector2>().x, 0, Variables.movementAction.ReadValue<Vector2>().y).normalized * Variables.moveSpeed;
    }

    public override void UpdateState()
    {
        // apply gravity to the player
        Variables.movementDirection.y += Variables.gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = Variables.movementDirection * Time.deltaTime;
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

        // switch the state to idle state if wasd movement not detected
        if (Variables.movementAction.ReadValue<Vector2>().magnitude <= 0f && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.idleState;
            player.currentState.EnterState();
        }

        
    }
}
