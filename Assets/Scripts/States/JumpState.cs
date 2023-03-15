using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{
    private Vector3 movementDirection;
    private float jumpVelocity;
    public override void EnterState(StateManager player)
    {
        Debug.Log("Jump State Entered");

        movementDirection = new Vector3(player.movementAction.ReadValue<Vector2>().x, 0, player.movementAction.ReadValue<Vector2>().y).normalized * player.var.moveSpeed;

        // calculate the jump velocity based on jump height and gravity
        jumpVelocity = Mathf.Sqrt(2 * player.var.jumpHeight * -player.var.gravity);
        movementDirection.y = jumpVelocity;

        // apply the jump velocity to the player
        player.controller.Move(player.transform.TransformDirection(movementDirection) * Time.deltaTime);
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        // apply gravity to the player
        movementDirection.y += player.var.gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = movementDirection * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        // if the player lands, transition back to idle state
        if (player.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
    }
}
