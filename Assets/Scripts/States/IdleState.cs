using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{

    public override void EnterState(StateManager player)
    {
        Debug.Log("Idle State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        player.var.verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(0, player.var.verticalVelocity * Time.deltaTime, 0).normalized * Time.deltaTime;
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

        // switch the state to crouch state if crouch key is pressed
        if (player.var.crouchAction.ReadValue<float>() > 0 && player.var.controller.isGrounded)
        {
            player.SwitchState(player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.var.jumpAction.triggered && player.var.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to fall state
        if (!(player.var.jumpAction.triggered) && !player.var.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
