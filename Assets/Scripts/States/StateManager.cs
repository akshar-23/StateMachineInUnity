using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{

    public BaseState currentState;
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
        Variables.controller = GetComponent<CharacterController>();
        
        
        currentState.EnterState(this);
        
        
        Variables.controls = new InputControls();
        Variables.controls.Enable();



        Variables.movementAction = Variables.controls.Player.Movement;
        Variables.sprintAction = Variables.controls.Player.Sprint;
        Variables.jumpAction = Variables.controls.Player.Jump;
        Variables.crouchAction = Variables.controls.Player.Crouch;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
