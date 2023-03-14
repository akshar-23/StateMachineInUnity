using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputmanager : MonoBehaviour
{

    private InputControls playerInput;
    private InputControls.PlayerActions player;

    private playercam look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new InputControls();
        player = playerInput.Player;
        look = GetComponent<playercam>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }
}
