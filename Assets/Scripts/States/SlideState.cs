using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : BaseState
{
    private float slideDuration = 0.25f;
    private float slideStartTime;
    private Vector3 slideDirection;

    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Slide State Entered!");
        player.transform.localScale = Vector3.one * 0.35f;
        slideStartTime = Time.time;
        Vector2 movementInput = player.movementAction.ReadValue<Vector2>();
        slideDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

        // move the player in the slide direction
        Vector3 movement = slideDirection * sprintSpeed * 2f * Time.deltaTime;
        player.controller.Move(player.transform.TransformDirection(movement));

        // check if the slide duration is over and switch back to the sprint state
        if (Time.time - slideStartTime >= slideDuration)
        {
            player.transform.localScale = Vector3.one;
            player.SwitchState(player.sprintState);
        }
    }
}
