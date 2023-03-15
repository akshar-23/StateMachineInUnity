using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Slide State Entered!");
        player.transform.localScale = Vector3.one * 0.35f;
        player.var.slideStartTime = Time.time;
        Vector2 movementInput = player.var.movementAction.ReadValue<Vector2>();
        player.var.slideDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
    }

    public override void UpdateState(StateManager player)
    {
        // move the player in the slide direction
        Vector3 movement = player.var.slideDirection * player.var.sprintSpeed * 2f * Time.deltaTime;
        player.var.controller.Move(player.transform.TransformDirection(movement));

        if (player.var.controller.isGrounded && player.var.verticalVelocity < 0f)
        {
            player.var.verticalVelocity = 0f;
        }

        // check if the slide duration is over and switch back to the sprint state
        if (Time.time - player.var.slideStartTime >= player.var.slideTime)
        {
            player.transform.localScale = Vector3.one;
            player.SwitchState(player.sprintState);
        }
    }
}
