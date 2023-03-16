using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputmanager : MonoBehaviour
{
    private playercam look;

    // Start is called before the first frame update
    void Awake()
    {
        Variables.controls = new InputControls();
        Variables.player = Variables.controls.Player;
        look = GetComponent<playercam>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        look.ProcessLook(Variables.player.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        Variables.player.Enable();
    }

    private void OnDisable()
    {
        Variables.player.Disable();
    }
}
