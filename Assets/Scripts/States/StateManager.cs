using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public Variables var;

    BaseState currentState;
    public WalkState walkState = new WalkState();
    public IdleState idleState = new IdleState();
    public SprintState sprintState = new SprintState();
    public CrouchState crouchState = new CrouchState();
    public JumpState jumpState = new JumpState();
    public SlideState slideState = new SlideState();
    public FallState fallState = new FallState();





    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        var.controller = GetComponent<CharacterController>();
        currentState.EnterState(this);
        var.controls = new InputControls();
        var.controls.Enable();



        var.movementAction = var.controls.Player.Movement;
        var.sprintAction = var.controls.Player.Sprint;
        var.jumpAction = var.controls.Player.Jump;
        var.crouchAction = var.controls.Player.Crouch;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
