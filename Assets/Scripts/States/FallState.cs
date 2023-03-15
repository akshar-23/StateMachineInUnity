using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : BaseState
{
    private Vector3 movementDirection;
    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Fall State Entered!");

        movementDirection = new Vector3(player.movementAction.ReadValue<Vector2>().x, 0, player.movementAction.ReadValue<Vector2>().y).normalized * moveSpeed;

        player.controller.Move(player.transform.TransformDirection(movementDirection) * Time.deltaTime);
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        // apply gravity to the player
        movementDirection.y += gravity * Time.deltaTime;

        // move the player based on the current jump velocity
        Vector3 movement = movementDirection * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));


        if (player.controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }

        // switch the state to walking state if wasd movement detected
        if (player.movementAction.ReadValue<Vector2>().magnitude > 0 && player.controller.isGrounded)
        {
            player.SwitchState(player.walkState);
        }

        // switch the state to idle state if wasd movement not detected
        if (player.movementAction.ReadValue<Vector2>().magnitude <= 0f && player.controller.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
    }
}
