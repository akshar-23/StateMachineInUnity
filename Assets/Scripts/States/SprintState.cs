using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprintState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Sprint State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * sprintSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        if (player.sprintAction.ReadValue<float>() <= 0)
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.jumpAction.triggered)
        {
            player.SwitchState(player.jumpState);
        }
    }
}
