using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public CharacterController controller;
    public InputControls controls;

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
