using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        // Calculate movement direction based on input
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;

        // Move the character
        transform.Translate(movement, Space.Self);
    }

    void Look()
    {
        // Read mouse input
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        // Update rotation based on mouse input
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera vertically (up/down)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player's body horizontally (left/right)
        playerBody.Rotate(Vector3.up * mouseDelta.x);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read movement input value
        moveInput = context.ReadValue<Vector2>();
    }
}