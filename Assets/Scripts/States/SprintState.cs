using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprintState : BaseState
{
    private InputAction sprintAction;

    public override void EnterState(StateManager player)
    {
        player.controls = new InputControls();
        player.controls.Enable();
        sprintAction = player.controls.Player.Sprint;
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);
        if(sprintAction.ReadValue<float>() > 0f)
        {
            Vector2 movementInput = player.controls.Player.Movement.ReadValue<Vector2>();

            Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * sprintSpeed * Time.deltaTime;
            player.controller.Move(player.transform.TransformDirection(movement));
        }
    }
}
