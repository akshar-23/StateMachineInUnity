using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState()
    {

        Debug.Log("Crouch State Entered!");
    }

    public override void UpdateState()
    {
        player.transform.localScale = Vector3.one * 0.5f;
        Vector3 movement = new Vector3(Variables.movementAction.ReadValue<Vector2>().x, Variables.verticalVelocity * Time.deltaTime, Variables.movementAction.ReadValue<Vector2>().y).normalized * Variables.moveSpeed * Time.deltaTime;
        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        if (Variables.crouchAction.ReadValue<float>() <= 0)
        {
            player.transform.localScale = Vector3.one;

            player.currentState.SwitchState();
        }
    }

    public override void SwitchState()
    {
        // switch to idle state
        if (Variables.movementAction.ReadValue<Vector2>().magnitude <= 0f && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.idleState;
            player.currentState.EnterState();
        }

        // switch to walk state
        if (Variables.movementAction.ReadValue<Vector2>().magnitude > 0 && Variables.controller.isGrounded)
        {
            player.currentState.ExitState();
            player.currentState = player.walkState;
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
