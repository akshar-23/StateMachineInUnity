using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        player.transform.localScale = Vector3.one * 0.5f;
        Debug.Log("Crouch State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();

        verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.moveSpeed * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        if (player.controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }

        if (player.crouchAction.ReadValue<float>() <= 0)
        {
            player.transform.localScale = Vector3.one;

            // switch to idle state
            if (movementInput.magnitude <= 0f && player.controller.isGrounded)
            {
                player.SwitchState(player.idleState);
            }

            // switch to walk state
            if (player.movementAction.ReadValue<Vector2>().magnitude > 0 && player.controller.isGrounded)
            {
                player.SwitchState(player.walkState);
            }

            // switch to fall state
            if (!(player.jumpAction.triggered) && !player.controller.isGrounded)
            {
                player.SwitchState(player.fallState);
            }
        }
    }
}
