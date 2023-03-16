using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercam : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // up and down looking
        Variables.xRotation -= (mouseY * Time.deltaTime) * Variables.ySens;
        Variables.xRotation = Mathf.Clamp(Variables.xRotation, -90f, 90f);
        // apply this to our camera transform
        cam.transform.localRotation = Quaternion.Euler(Variables.xRotation, 0f, 0f);
        // rotate player left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * Variables.xSens);

    }
}
