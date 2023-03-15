using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputmanager : MonoBehaviour
{
    public Variables var;
    private playercam look;

    // Start is called before the first frame update
    void Awake()
    {
        var.controls = new InputControls();
        var.player = var.controls.Player;
        look = GetComponent<playercam>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        look.ProcessLook(var.player.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        var.player.Enable();
    }

    private void OnDisable()
    {
        var.player.Disable();
    }
}
