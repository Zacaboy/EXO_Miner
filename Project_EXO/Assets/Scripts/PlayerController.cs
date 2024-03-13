using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechController : MonoBehaviour
{
    public static PlayerMechController me;

    public float moveSpeed = 5f; // Speed of movement
    public float lookSpeed = 2f; // Speed of looking

    private Vector2 movementInput; // Input for movement
    private Vector2 lookInput; // Input for looking
    public float upLimit = 45;
    public float downLimit = -45;
    int jumping;
    float airTime;
    public bool grounded;
    bool wasGrounded;
    float landedTime;
    public float height = 5;
    public Collider legsCol;
    public Vector3 centrePoint = new Vector3(45, 0, 0);

    [Header("Jump Stats")]
    public float jumpSpeed = 0.4f;
    public float jumpHeight = 6000;
    public float jumpForward = 6000;
    public float fallSpeed = 0.2f;
    public float landingTime = 1.5f;

    [Header("Head Bob")]
    [Range(0.001f, 0.04f)]
    public float amount = 0.002f;
    [Range(1, 30)]
    public float frequency = 10;

    [Range(10, 100)]
    public float smooth = 10;

    [Header("Ignore")]
    float drag;
    float rotX;
    float rotY;
    Vector3 startPos;
    [HideInInspector] public Transform lookpos;

    void Start()
    {
        me = this;
        drag = GetComponent<Rigidbody>().drag;
        legsCol.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rotX = transform.eulerAngles.y;
        rotY = Camera.main.transform.localEulerAngles.x;
        startPos = Camera.main.transform.localPosition;
        lookpos = new GameObject().transform;
        lookpos.transform.position = Camera.main.transform.position;
        lookpos.transform.eulerAngles = Camera.main.transform.eulerAngles;
        lookpos.transform.position += transform.forward * centrePoint.x;
        lookpos.SetParent(Camera.main.transform);
        lookpos.name = "Look Pos";
    }

    void FixedUpdate()
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
        if (Time.time < 1)
        {
            lookHorizontal = 0;
            lookVertical = 0;
        }

        // Mouse rotation
        rotY += lookVertical * -lookSpeed * Time.fixedDeltaTime / 2;
        rotY = Mathf.Clamp(rotY, downLimit, upLimit);

        // Camera rotation
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, Quaternion.Euler(rotY, 0f, 0f), 0.1f);

        if (jumping == 0 & landedTime == 0 & grounded)
        {
            // Move the GameObject
            GetComponent<Rigidbody>().AddForce(movement * moveSpeed);

            // Checks if moving for the camera bob
            if (movement.magnitude > 0)
                StartHeadbob();

            // Mouse rotation
            rotX += lookHorizontal * lookSpeed * Time.fixedDeltaTime;

            // Camera rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, rotX, 0f).normalized, 0.04f);
        }
        StopHeadbob();
    }

    // Starts the camera headbob
    public Vector3 StartHeadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2) * amount * 1.6f, smooth * Time.deltaTime);
        Camera.main.transform.parent.localPosition += pos;
        return pos;
    }

    // Stops the camera headbob
    public void StopHeadbob()
    {
        if (Camera.main.transform.parent.localPosition == startPos) return;
        Camera.main.transform.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.localPosition, startPos, 1 * 1 * Time.deltaTime);
    }

    void Update()
    {
        // This checks if on the ground Camera.main.transform.parent
        RaycastHit CheckGround = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out CheckGround, height))
        {
            grounded = true;
            GetComponent<Rigidbody>().drag = drag;
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
            GetComponent<Rigidbody>().drag = fallSpeed;
        }

        // This calls the landing sequence
        if (Time.time > 0.5f & grounded & !wasGrounded & airTime > 0)
        {
            landedTime = Time.time;
            legsCol.enabled = true;
            // Checks how long you are in the air for
            if (airTime > 0.8f)
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
            Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
        else
        {
            if (landedTime > 0 & Time.time > landedTime + 0.08f)
                Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
            else
                Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, 0, 0), 0.01f);
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

