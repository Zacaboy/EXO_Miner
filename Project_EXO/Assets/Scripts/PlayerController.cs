using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float lookSpeed = 2f; // Speed of looking

    private Vector2 movementInput; // Input for movement
    private Vector2 lookInput; // Input for looking
    int jumping;
    float airTime;
    bool grounded;
    bool wasGrounded;
    float landedTime;
    public float height = 5;
    public Collider legsCol;

    [Header("Jump Stats")]
    public float jumpSpeed = 0.4f;
    public float jumpHeight = 6000;
    public float jumpForward = 6000;
    public float landingTime = 1.5f;

    [Header("Ignore")]
    float drag;

    private void Start()
    {
        drag = GetComponent<Rigidbody>().drag;
        legsCol.enabled = false;
    }

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

        // Get look input
        float lookHorizontal = lookInput.x;
        float lookVertical = lookInput.y;

        // Calculate rotation angles based on input
        float yaw = lookHorizontal * lookSpeed * Time.deltaTime;
        float pitch = -lookVertical * lookSpeed * Time.deltaTime; // Inverted for typical first-person controls

        if (jumping == 0 & landedTime == 0 & grounded)
        {
            // Rotate the GameObject
            transform.Rotate(Vector3.up, yaw);

            // Move the GameObject
            GetComponent<Rigidbody>().AddForce(movement * moveSpeed);
            // transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            // Get the current camera rotation
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Apply pitch rotation to the camera
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(cameraRotation.eulerAngles.x + pitch, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z), 0.8f);
        }

        // THis checks if on the ground
        RaycastHit CheckGround = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out CheckGround, height))
        {
            grounded = true;
            GetComponent<Rigidbody>().drag = drag;
            Debug.Log(CheckGround.transform.name);
            if (!wasGrounded)
            {
                if (CheckGround.transform.GetComponent<Bug>())
                    CheckGround.transform.GetComponent<Bug>().Kill();
            }
        }
        else
        {
            // This determines how long you are in the air for for fall damage or a bigger landing effect
            grounded = false;
            airTime = Time.time;
            GetComponent<Rigidbody>().drag = 0.3f;
        }

        // This calls the landing sequence
        if (Time.time > 0.5f & grounded & !wasGrounded & airTime > 0)
        {
            landedTime = Time.time;
            legsCol.enabled = true;
            // Checks how long you are in the air for
            if (airTime > 0)
                GetComponentInChildren<Animator>().CrossFadeInFixedTime("Land", 0.1f);
        }

        // This resets the landing sequence after it is done
        if (Time.time >= landedTime + landingTime)
            landedTime = 0;
        if (Time.time >= landedTime + 0.1f)
            legsCol.enabled = false;
        wasGrounded = grounded;

        // This controls where the camera parent is and the camera shake
        if (jumping == 1)
            Camera.main.transform.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
        else
        {
            if (landedTime > 0 & Time.time > landedTime + 0.08f)
                Camera.main.transform.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
            else
                Camera.main.transform.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.localPosition, new Vector3(0, 0, 0), 0.01f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get movement input from joystick
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumping == 0 & jumping == 0 & landedTime == 0 & grounded)
            StartCoroutine(WaitJump());
    }
    // Make sure you call a StartCoroutine instead of a regular void
    public IEnumerator WaitJump()
    {
        jumping = 1;
        GetComponentInChildren<Animator>().CrossFadeInFixedTime("Jump", 0.1f);
        yield return new WaitForSeconds(jumpSpeed);
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * jumpForward);
        jumping = 0;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Get look input from joystick
        lookInput = context.ReadValue<Vector2>();
    }
}

