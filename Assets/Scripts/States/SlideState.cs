using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : BaseState
{
    public override void EnterState(StateManager player)
    {
        Debug.Log("Slide State Entered!");
        player.transform.localScale = Vector3.one * 0.35f;
        Variables.slideStartTime = Time.time;
        Vector2 movementInput = Variables.movementAction.ReadValue<Vector2>();
        Variables.slideDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
    }

    public override void UpdateState(StateManager player)
    {
        // move the player in the slide direction
        Vector3 movement = Variables.slideDirection * Variables.sprintSpeed * 2f * Time.deltaTime;
        Variables.controller.Move(player.transform.TransformDirection(movement));

        if (Variables.controller.isGrounded && Variables.verticalVelocity < 0f)
        {
            Variables.verticalVelocity = 0f;
        }

        // check if the slide duration is over and switch back to the sprint state
        if (Time.time - Variables.slideStartTime >= Variables.slideTime)
        {
            player.transform.localScale = Vector3.one;
            SwitchState(player, player.sprintState);
        }
    }

    public override void SwitchState(StateManager player, BaseState newState)
    {
        player.currentState.ExitState(player);
        player.currentState = newState;
        player.currentState.EnterState(player);
    }
}
