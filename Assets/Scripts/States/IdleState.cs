using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{

    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Idle State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);
        Vector2 movementInput = movementAction.ReadValue<Vector2>();

        // switch the state to walking state if wasd movement detected
        if (movementInput.magnitude > 0 )
        {
            player.SwitchState(player.walkState);
        }
    }
}
