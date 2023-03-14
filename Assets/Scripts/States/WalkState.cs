using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : BaseState
{
    public override void EnterState(StateManager player)
    {
        player.controls = new InputControls();
        player.controls.Enable();
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * moveSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));
    }

    public override void ExitState(StateManager player)
    {
        player.controls.Disable();
    }
}
