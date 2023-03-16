using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{
    public override void EnterState()
    {
        Debug.Log("Jump State Entered");

        Variables.movementDirection = new Vector3(Variables.movementAction.ReadValue<Vector2>().x, 0, Variables.movementAction.ReadValue<Vector2>().y).normalized * Variables.moveSpeed;

        // calculate the jump velocity based on jump height and gravity
        Variables.jumpVelocity = Mathf.Sqrt(2 * Variables.jumpHeight * -Variables.gravity);
        Variables.movementDirection.y = Variables.jumpVelocity;
    }

    public override void UpdateState()
    {
        Variables.movementDirection.y += Variables.gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = Variables.movementDirection * Time.deltaTime;
        Variables.controller.Move(player.transform.TransformDirection(movement));

        player.currentState.SwitchState();
    }

    public override void SwitchState()
    {
        // if the player lands, transition back to idle state
        if (Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.idleState;
            player.currentState.EnterState();
        }
    }
}
