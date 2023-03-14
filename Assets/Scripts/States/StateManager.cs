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

    // Start is called before the first frame update
    void Start()
    {
        currentState = walkState;
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
