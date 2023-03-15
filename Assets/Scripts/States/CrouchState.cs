using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Crouch State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        player.transform.localScale = Vector3.one * 0.5f;

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized * moveSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        if(player.crouchAction.ReadValue<float>() <= 0)
        {
            player.transform.localScale = Vector3.one;

            // switch to idle state
            if (movementInput.magnitude <= 0f)
            {
                player.SwitchState(player.idleState);
            }

            // switch to walk state
            if (player.movementAction.ReadValue<Vector2>().magnitude > 0)
            {
                player.SwitchState(player.walkState);
            }
        }
    }
}
