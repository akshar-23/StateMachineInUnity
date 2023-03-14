using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprintState : BaseState
{
    private InputAction sprintMovement;
    public override void EnterState(StateManager player)
    {
        sprintMovement = player.controls.Player.Sprint;
    }

    public override void UpdateState(StateManager player)
    {
        base.UpdateState(player);

    }

    public override void ExitState(StateManager player)
    {

    }
}
