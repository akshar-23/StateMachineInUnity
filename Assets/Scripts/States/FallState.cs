using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Fall State Entered!");

        player.var.movementDirection = new Vector3(player.var.movementAction.ReadValue<Vector2>().x, 0, player.var.movementAction.ReadValue<Vector2>().y).normalized * player.var.moveSpeed;

        player.var.controller.Move(player.transform.TransformDirection(player.var.movementDirection) * Time.deltaTime);
    }

    public override void UpdateState(StateManager player)
    {
        // apply gravity to the player
        player.var.movementDirection.y += player.var.gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = player.var.movementDirection * Time.deltaTime;
        player.var.controller.Move(player.transform.TransformDirection(movement));


        if (player.var.controller.isGrounded && player.var.verticalVelocity < 0f)
        {
            player.var.verticalVelocity = 0f;
        }

        // switch the state to walking state if wasd movement detected
        if (player.var.movementAction.ReadValue<Vector2>().magnitude > 0 && player.var.controller.isGrounded)
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to idle state if wasd movement not detected
        if (player.var.movementAction.ReadValue<Vector2>().magnitude <= 0f && player.var.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
    }
}
