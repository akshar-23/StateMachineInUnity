using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class BaseState
{
    public virtual void EnterState(StateManager player) { }

    public virtual void UpdateState(StateManager player) { }

    public virtual void SwitchState(StateManager player, BaseState state) { }

    public virtual void ExitState(StateManager player) { }
}
