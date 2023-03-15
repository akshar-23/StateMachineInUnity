using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState(StateManager player)
    {
        player.transform.localScale = Vector3.one * 0.5f;
        Debug.Log("Crouch State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        Vector2 movementInput = player.var.movementAction.ReadValue<Vector2>();

        player.var.verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(movementInput.x, player.var.verticalVelocity * Time.deltaTime, movementInput.y).normalized * player.var.moveSpeed * Time.deltaTime;
        player.var.controller.Move(player.transform.TransformDirection(movement));

        if (player.var.controller.isGrounded && player.var.verticalVelocity < 0f)
        {
            player.var.verticalVelocity = 0f;
        }

        if (player.var.crouchAction.ReadValue<float>() <= 0)
        {
            player.transform.localScale = Vector3.one;

            // switch to idle state
            if (movementInput.magnitude <= 0f && player.var.controller.isGrounded)
            {
                player.SwitchState(player.idleState);
            }

            // switch to walk state
            if (player.var.movementAction.ReadValue<Vector2>().magnitude > 0 && player.var.controller.isGrounded)
            {
                player.SwitchState(player.walkState);
            }

            // switch to fall state
            if (!(player.var.jumpAction.triggered) && !player.var.controller.isGrounded)
            {
                player.SwitchState(player.fallState);
            }
        }
    }
}
