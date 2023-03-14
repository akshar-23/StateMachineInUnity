using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Walking State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = movementAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * moveSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        // switch the state to idle state if wasd movement not detected
        if (movementInput.magnitude == 0f)
        {
            player.SwitchState(player.idleState);
        }
    }
}
