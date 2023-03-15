using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Walking State Entered!");
        player.sprintAction.Enable();
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * moveSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        // switch the state to idle state if wasd movement not detected
        if (movementInput.magnitude <= 0f)
        {
            player.SwitchState(player.idleState);
            player.sprintAction.Disable();
        }

        // switch to sprint state
        if(player.sprintAction.ReadValue<float>() > 0)
        {
            player.SwitchState(player.sprintState);
        }

        // switch to crouch state
        if (player.crouchAction.ReadValue<float>() > 0)
        {
            player.SwitchState(player.crouchState);
        }
    }
}
