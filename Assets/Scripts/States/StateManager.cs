using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public CharacterController controller;
    public InputControls controls;

    public InputAction movementAction;
    public InputAction sprintAction;
    public InputAction jumpAction;
    public InputAction crouchAction;

    BaseState currentState;
    public WalkState walkState = new WalkState();
    public IdleState idleState = new IdleState();
    public SprintState sprintState = new SprintState();
    public CrouchState crouchState = new CrouchState();
    public JumpState jumpState = new JumpState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        controller = GetComponent<CharacterController>();
        currentState.EnterState(this);
        controls = new InputControls();
        controls.Enable();



        movementAction = controls.Player.Movement;
        sprintAction = controls.Player.Sprint;
        jumpAction = controls.Player.Jump;
        crouchAction = controls.Player.Crouch;
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
