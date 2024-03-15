using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechController : MonoBehaviour
{
    public static PlayerMechController me;

    [Header("Movement")]
    public float moveSpeed = 5f; // Speed of movement
    public float lookSpeed = 2f; // Speed of looking

    [Header("Head Bob")]
    [Range(0.001f, 0.04f)]
    public float amount = 0.002f;
    [Range(1, 30)]
    public float frequency = 10;
    [Range(10, 100)]
    public float smooth = 10;

    [Header("SFX")]
    public AudioClip[] walkingSFX;
    public AudioClip[] jumpingSFX;
    public AudioClip[] landingSFX;
    public AudioClip[] lookSFX;
    public AudioClip[] downSFX;
    public float walkFrequency = 0.5f;
    public float lookFrequency = 0.5f;

    [Header("Look Limits")]
    public float lookUpLimit = 45;
    public float lookDownLimit = -45;

    [Header("Misc")]
    public Vector3 centrePoint = new Vector3(45, 0, 0);
    public Transform lightningPos;

    [Header("Jump Stats")]
    public float jumpSpeed = 0.4f;
    public float jumpHeight = 6000;
    public float jumpForward = 6000;
    public float playerFallSpeed = 0.05f;
    public float landingTime = 1.5f;
    public float[] landingForces;
    public float killHeight = -200;

    [Header("Jump Stats")]
    public Animator cameraAni;
    public Animator cameraShakeAni;

    [Header("Landing")]
    public float landingDamageAreaEffect = 50;
    public float landingDamage = 50;
    public ParticleSystem landingVFX;

    [Header("Cockpit Lights")]
    public Light[] lightingLights;
    public Color lightningWarningColour = Color.yellow;
    public float lightningWarningFlashFrequency = 0.5f;
    public AudioClip lightningWarningSFX;
    public Color lightningDangerColour = Color.red;
    public float lightningDangerFlashFrequency = 0.8f;
    public AudioClip lightningDangerSFX;

    [Header("Ignore")]
    float drag;
    float rotX;
    float rotY;
    Vector3 startPos;
    private Vector2 movementInput; // Input for movement
    private Vector2 lookInput; // Input for looking
    [HideInInspector] public Transform lookpos;
    FeetScript feetScript;
    [HideInInspector] public Rigidbody rigi;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool warningLightning;
    [HideInInspector] public bool dangerLightning;
    bool stunned;
    bool jumpAni;
    int jumping;
    float airTime;
    bool wasGrounded;
    [HideInInspector] public bool lightningStruckFall;
    float landedTime;
    float lastWalkTime;
    float lastLookTime;
    float lastLightningTime;

    void Start()
    {
        me = this;
        rigi = GetComponent<Rigidbody>();
        drag = rigi.drag;
        feetScript = GetComponentInChildren<FeetScript>();
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
        for (int i = 0; i < lightingLights.Length; i++)
        {
            lightingLights[i].intensity = 0;
            lightingLights[i].enabled = true;
        }
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
        bool looking = false;

        // Get look input
        float lookHorizontal = lookInput.x;
        float lookVertical = lookInput.y;
        if (lookVertical != 0)
            looking = true;
        if (Time.time < 1)
        {
            lookHorizontal = 0;
            lookVertical = 0;
        }

        // Mouse rotation
        rotY += lookVertical * -lookSpeed * Time.fixedDeltaTime / 2;
        rotY = Mathf.Clamp(rotY, lookDownLimit, lookUpLimit);

        // Camera rotation
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, Quaternion.Euler(rotY, 0f, 0f), 0.1f);

        if (jumping == 0 & landedTime == 0 & grounded)
        {
            // Move the GameObject
            rigi.AddForce(movement * moveSpeed);

            // Checks if moving for the camera bob
            if (movement.magnitude > 0)
            {
                if(Time.time >= lastWalkTime + walkFrequency)
                {
                    lastWalkTime = Time.time;
                    if (walkingSFX.Length > 0)
                        FXManager.SpawnSFX(walkingSFX[Random.Range(0, walkingSFX.Length - 1)], transform.position, 50, 5);
                    if (!stunned)
                        cameraShakeAni.CrossFadeInFixedTime("Walk", 0.1f);  
                }
                StartHeadbob();
            }

            // Mouse rotation
            rotX += lookHorizontal * lookSpeed * Time.fixedDeltaTime;
            if (lookHorizontal != 0)
                looking = true;

            // Camera rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, rotX, 0f).normalized, 0.04f);
        }
        else
            lastWalkTime = Time.time;
        if (looking)
        {
            if (Time.time >= lastLookTime + lookFrequency)
            {
                lastLookTime = Time.time;
                if (lookSFX.Length > 0)
                    FXManager.SpawnSFX(lookSFX[Random.Range(0, lookSFX.Length - 1)], transform.position, 10, 5);
            }
        }
        else
            lastLookTime = Time.time;
        StopHeadbob();
    }

    void Update()
    {
        // This checks if on the ground Camera.main.transform.parent
        if (feetScript.ground.Count > 0)
        {
            grounded = true;
            rigi.drag = drag;
            lightningStruckFall = false;
            if (wasGrounded)
                airTime = Time.time;
        }
        else
        {
            // This determines how long you are in the air for for fall damage or a bigger landing effect
            grounded = false;
            rigi.drag = playerFallSpeed;
        }
        if (transform.position.y <= killHeight & !GameManager.me.over)
            GameManager.me.FailObjective();

        // This is for the warning lights in the cockpit
        if (warningLightning || dangerLightning)
        {
            if (dangerLightning)
            {
                lightingLights[0].color = lightningDangerColour;
                if (Time.time >= lastLightningTime + lightningDangerFlashFrequency)
                {
                    lastLightningTime = Time.time;
                    lightingLights[0].intensity = Mathf.Lerp(lightingLights[0].intensity, 3, 0.1f);
                    if (lightningDangerSFX)
                        FXManager.SpawnSFX(lightningWarningSFX, lightingLights[0].transform.position, 10, 2);
                }
                else
                    lightingLights[0].intensity = Mathf.Lerp(lightingLights[0].intensity, 0, 0.1f);
            }
            else
            {
                if (warningLightning)
                {
                    lightingLights[0].color = lightningWarningColour;
                    if (Time.time >= lastLightningTime + lightningWarningFlashFrequency)
                    {
                        lastLightningTime = Time.time;
                        lightingLights[0].intensity = Mathf.Lerp(lightingLights[0].intensity, 3, 0.1f);
                        if (lightningWarningSFX)
                            FXManager.SpawnSFX(lightningWarningSFX, lightingLights[0].transform.position, 10, 2);
                    }
                    else
                        lightingLights[0].intensity = Mathf.Lerp(lightingLights[0].intensity, 0, 0.1f);
                }
            }
        }
        else
        {
            lightingLights[0].intensity = Mathf.Lerp(lightingLights[0].intensity, 0, 0.1f);
            lastLightningTime = 0;
        }
        for (int i = 1; i < lightingLights.Length; i++)
        {
            lightingLights[i].intensity = Mathf.Lerp(lightingLights[i].intensity, lightingLights[0].intensity, 0.1f);
            lightingLights[i].color = Color.Lerp(lightingLights[i].color, lightingLights[0].color, 0.1f);
        }

        // This calls the landing sequence
        if (Time.timeSinceLevelLoad > 0.5f & grounded & !wasGrounded || stunned)
        {
            if (Time.time >= airTime + landingForces[0] || stunned)
            {
                if (!jumpAni)
                {
                    jumpAni = true;
                    landedTime = Time.time;
                    if (!stunned)
                    {
                        lightningStruckFall = false;
                        cameraShakeAni.CrossFadeInFixedTime("Land", 0.1f);
                        // For Squishing Bugs
                        foreach (Bug bug in FindObjectsOfType<Bug>())
                            if (Vector3.Distance(transform.position, bug.transform.position) <= landingDamageAreaEffect)
                                bug.Kill();
                        if (landingSFX.Length > 0)
                            FXManager.SpawnSFX(landingSFX[Random.Range(0, landingSFX.Length - 1)], transform.position, 100, 5);
                        if (landingVFX)
                            FXManager.SpawnVFX(landingVFX, feetScript.transform.position, transform.eulerAngles, 15);
                    }
                    else
                    {
                        cameraShakeAni.CrossFadeInFixedTime("Lightning", 0.1f);
                        landedTime = Time.time + 2.3f;
                    }
                    string landAni = "Land";
                    if (Time.time >= airTime + landingForces[1])
                    {
                        landedTime = Time.time;
                        landAni = "Land";
                    }
                    cameraAni.CrossFadeInFixedTime(landAni, 0.1f);
                }
            }
            else
                landedTime = 0;
            stunned = false;
        }

        // This resets the landing sequence after it is done
        if (Time.time >= landedTime + landingTime)
        {
            jumpAni = false;
            dangerLightning = false;
            landedTime = 0;
        }
        wasGrounded = grounded;

        // This controls where the camera parent is and the camera shake
        if (jumping == 1)
            Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
        else
        {
            if (landedTime > 0 & Time.time > landedTime + 0.1f)
                Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, -0.15f, 0), 0.01f);
            else
                Camera.main.transform.parent.parent.localPosition = Vector3.Lerp(Camera.main.transform.parent.parent.localPosition, new Vector3(0, 0, 0), 0.01f);
        }
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

    public void Stun(bool lightning = false)
    {
        stunned = true;
        if (lightning)
            dangerLightning = true;
        lightningStruckFall = true;
        if (downSFX.Length > 0)
            FXManager.SpawnSFX(downSFX[Random.Range(0, downSFX.Length - 1)], transform.position, 10, 5, 0.3f);
    }

    public void FireWeapon()
    {
        cameraShakeAni.CrossFadeInFixedTime("Cannon", 0.1f);
    }
    public void FireMiningLazer(bool hit)
    {
        if (hit)
            cameraShakeAni.CrossFadeInFixedTime("Mining_Lazer_Hit", 0.1f);
        else
            cameraShakeAni.CrossFadeInFixedTime("Mining_Lazer", 0.1f);
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
        cameraAni.CrossFadeInFixedTime("Jump", 0.1f);
        yield return new WaitForSeconds(jumpSpeed);
        rigi.AddForce(Vector3.up * jumpHeight);
        rigi.AddForce(Camera.main.transform.forward * jumpForward);
        lightningStruckFall = false;
        if (jumpingSFX.Length > 0)
            FXManager.SpawnSFX(jumpingSFX[Random.Range(0, jumpingSFX.Length - 1)], transform.position, 50, 5);
        jumping = 0;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Get look input from joystick
        lookInput = context.ReadValue<Vector2>();
    }
}

