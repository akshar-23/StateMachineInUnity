using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{

    public override void EnterState(StateManager player)
    {
        base.EnterState(player);
        Debug.Log("Idle State Entered!");
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);
        verticalVelocity += player.var.gravity * Time.deltaTime;

        Vector3 movement = new Vector3(0, verticalVelocity * Time.deltaTime, 0).normalized * Time.deltaTime;
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

        // switch the state to crouch state if crouch key is pressed
        if (player.crouchAction.ReadValue<float>() > 0 && player.controller.isGrounded)
        {
            player.SwitchState(player.crouchState);
        }

        // switch the state to jump state if jump key is pressed
        if (player.jumpAction.triggered && player.controller.isGrounded)
        {
            player.SwitchState(player.jumpState);
        }

        // switch to fall state
        if (!(player.jumpAction.triggered) && !player.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
