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

        // switch the state to walking state if wasd movement detected
        if (player.movementAction.ReadValue<Vector2>().magnitude > 0 )
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to crouch state if crouch key is pressed
        if (player.crouchAction.ReadValue<float>() > 0)
        {
            player.SwitchState(player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.jumpAction.triggered)
        {
            player.SwitchState(player.jumpState);
        }
    }
}
