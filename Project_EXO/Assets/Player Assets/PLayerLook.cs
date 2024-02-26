using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float lookSpeed = 2f; // Speed of looking

    private Vector2 lookInput; // Input for looking

    void Update()
    {
        // Get look input
        float lookHorizontal = lookInput.x;
        float lookVertical = lookInput.y;

        // Calculate rotation angles based on input
        float yaw = lookHorizontal * lookSpeed * Time.deltaTime;
        float pitch = -lookVertical * lookSpeed * Time.deltaTime; // Inverted for typical first-person controls

        // Rotate the GameObject
        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.right, pitch);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
