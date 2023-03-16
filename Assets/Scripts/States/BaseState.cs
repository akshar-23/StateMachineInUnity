using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class BaseState
{
    public StateManager player;
    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void SwitchState() { }

    public virtual void ExitState() { }
}
