using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float lookSpeed = 2f; // Speed of looking

    private Vector2 movementInput; // Input for movement
    private Vector2 lookInput; // Input for looking

    void Update()
    {
        // Get movement input
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 movement = forward * movementInput.y + right * movementInput.x;

        // Move the GameObject
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Get look input
        float lookHorizontal = lookInput.x;
        float lookVertical = lookInput.y;

        // Calculate rotation angles based on input
        float yaw = lookHorizontal * lookSpeed * Time.deltaTime;
        float pitch = -lookVertical * lookSpeed * Time.deltaTime; // Inverted for typical first-person controls

        // Rotate the GameObject
        transform.Rotate(Vector3.up, yaw);

        // Get the current camera rotation
        Quaternion cameraRotation = Camera.main.transform.rotation;

        // Apply pitch rotation to the camera
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation.eulerAngles.x + pitch, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get movement input from joystick
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Get look input from joystick
        lookInput = context.ReadValue<Vector2>();
    }
}
