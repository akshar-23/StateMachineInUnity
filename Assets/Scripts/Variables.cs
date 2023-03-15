using UnityEngine;
using UnityEngine.InputSystem;

public class Variables : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float slideDuration = 0.25f;
    public float verticalVelocity;
    public Vector3 movementDirection;
    public float jumpVelocity;
    public float slideTime = 0.25f;
    public float slideStartTime;
    public Vector3 slideDirection;


    public CharacterController controller;
    public InputControls controls;

    public InputAction movementAction;
    public InputAction sprintAction;
    public InputAction jumpAction;
    public InputAction crouchAction;

    public InputControls.PlayerActions player;
}
