using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

[Preserve]
public static class Variables
{
    public static float moveSpeed = 5f;
    public static float sprintSpeed = 10f;
    public static float gravity = -9.81f;
    public static float jumpHeight = 1.5f;
    public static float slideTime = 0.25f;
    public static Vector3 slideDirection;
    public static Vector3 movementDirection;
    public static float verticalVelocity;
    public static float jumpVelocity;
    public static float slideStartTime;
    public static CharacterController controller;
    public static InputControls controls;
    public static InputAction movementAction;
    public static InputAction sprintAction;
    public static InputAction jumpAction;
    public static InputAction crouchAction;
    public static InputControls.PlayerActions player;
}
