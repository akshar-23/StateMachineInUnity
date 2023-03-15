using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Jump State Entered");

        player.var.movementDirection = new Vector3(player.var.movementAction.ReadValue<Vector2>().x, 0, player.var.movementAction.ReadValue<Vector2>().y).normalized * player.var.moveSpeed;

        // calculate the jump velocity based on jump height and gravity
        player.var.jumpVelocity = Mathf.Sqrt(2 * player.var.jumpHeight * -player.var.gravity);
        player.var.movementDirection.y = player.var.jumpVelocity;

        // apply the jump velocity to the player
        player.var.controller.Move(player.transform.TransformDirection(player.var.movementDirection) * Time.deltaTime);
    }

    public override void UpdateState(StateManager player)
    {
        player.var.movementDirection.y += player.var.gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = player.var.movementDirection * Time.deltaTime;
        player.var.controller.Move(player.transform.TransformDirection(movement));

        // if the player lands, transition back to idle state
        if (player.var.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
    }
}
