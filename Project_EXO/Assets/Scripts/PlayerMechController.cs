using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Windows;
using UnityEngine.XR;

public class PlayerMechController : MonoBehaviour
{
    public static PlayerMechController me;

    [Header("Movement")]
    public float moveSpeedKeyboard = 230; // Speed of movement with keyboard
    public float moveSpeedJoystick = 230; // Speed of movementwith joystick

    [Header("Keyboard")]
    public float lookSpeedKeyboard = 20f; // Speed of looking with keyboard
    [Range(0.01f, 0.2f)]
    public float lookSmoothingXKeyboard = 0.05f;
    [Range(0.01f, 0.2f)]
    public float lookSmoothingYKeyboard = 0.05f;

    [Header("Joystick")]
    public float lookSpeedJoystick = 50f; // Speed of looking with joystick
    [Range(0.01f, 0.2f)]
    public float lookSmoothingXJoystick = 0.15f;
    [Range(0.01f, 0.2f)]
    public float lookSmoothingYJoystick = 0.15f;


    [Header("Head Bob")]
    [Range(0.001f, 0.04f)]
    public float amount = 0.002f;
    [Range(1, 30)]
    public float frequency = 10;
    [Range(10, 100)]
    public float smooth = 10;
    public ParticleSystem[] walksVFX;
    public Transform[] feet;

    [Header("SFX")]
    public AudioClip startSFX;
    public AudioClip[] walkingSFX;
    public AudioClip[] jumpingSFX;
    public AudioClip[] landingSFX;
    public AudioClip[] lookSFX;
    public AudioClip[] downSFX;
    public float walkFrequency = 0.5f;
    public float lookFrequency = 0.5f;

    [Header("Audio Sources")]
    public AudioSource[] walkingSources;
    public AudioSource wasteSource;

    [Header("Look Limits")]
    public float lookUpLimit = 45;
    public float lookDownLimit = -45;

    [Header("Misc")]
    public Vector3 centrePoint = new Vector3(45, 0, 0);
    public Transform lightningPos;

    [Header("Vaulting")]
    public Transform[] vaultPos;
    public float vaultForce = 300;

    [Header("Jump Stats")]
    public float jumpSpeed = 0.4f;
    public float jumpHeight = 6000;
    public float jumpForward = 6000;
    public float playerFallMass = 5f;
    public float playerFallDrag = 0.05f;
    public float landingTime = 1.5f;
    public float[] landingForces;
    public float killHeight = -200;

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

    [Header("Camera Effects")]
    public AnimationClip[] cameraAniList;
    public AnimationClip[] cameraShakeAniList;
    Animator cameraAniPref;
    Animator cameraShakeAniPref;
    Transform cameraPivot;
    List<Animator> cameraAni = new List<Animator>();
    List<Animator> cameraShakeAni = new List<Animator>();

    [Header("Ignore")]
    float drag;
    float mass;
    float rotX;
    float rotY;
    Vector3 startPos;
    private Vector2 movementInput; // Input for movement
    private Vector2 lookInput; // Input for looking
    [HideInInspector] public Transform lookpos;
    PlayerInput input;
    FeetScript feetScript;
    public Health health;
    [HideInInspector] public Rigidbody rigi;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool warningLightning;
    [HideInInspector] public bool dangerLightning;
    bool stunned;
    bool jumpAni;
    bool stunnedAni;
    int jumping;
    float airTime;
    bool wasGrounded;
    [HideInInspector] public bool lightningStruckFall;
    Transform lastCamEffect;
    int currentFoot;
    public float stunTime;
    float landedTime;
    float lastWalkTime;
    float lastLookTime;
    float lastLightningTime;
    [HideInInspector] public float lastStunTime;
    bool playedSFX;
    float startlandingDamageAreaEffect;
    bool startLand;
    public bool active;

    private void Awake()
    {
        me = this;
    }

    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        mass = rigi.mass;
        drag = rigi.drag;
        feetScript = GetComponentInChildren<FeetScript>();
        input = GetComponent<PlayerInput>();
        health = GetComponent<Health>();
        health.damageEvent.AddListener(Damage);
        health.deathEvent.AddListener(Die);
        startlandingDamageAreaEffect = landingDamageAreaEffect;
        landingDamageAreaEffect = 0;
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
        lastStunTime = -1;
        active = true;
        foreach (Animator ani in GetComponentsInChildren<Animator>())
        {
            if (ani.name == "Camera Ani")
                cameraAniPref = ani;
            if (ani.name == "Camera Shake")
                cameraShakeAniPref = ani;
        }
        cameraPivot = cameraShakeAniPref.transform.GetChild(0);
        for (int i = 0; i < lightingLights.Length; i++)
        {
            lightingLights[i].intensity = 0;
            lightingLights[i].enabled = true;
        }
        SetUpCameraInfo();
    }

    public IEnumerator SetUp()
    {
        startLand = true;
        input.enabled = false;
        yield return new WaitForSeconds(1);
        landingDamageAreaEffect = startlandingDamageAreaEffect;
        input.enabled = true;
    }

    public void SetUpCameraInfo()
    {
        cameraPivot.SetParent(transform);
        cameraShakeAniPref.transform.SetParent(transform);
        lastCamEffect = cameraAniPref.transform.parent;
        foreach (AnimationClip s in cameraAniList)
            cameraAni.Add(AddCameraInfo(s.name, cameraAniPref));
        foreach (AnimationClip s in cameraShakeAniList)
            cameraShakeAni.Add(AddCameraInfo(s.name, cameraShakeAniPref));
        cameraPivot.SetParent(lastCamEffect);
        Destroy(cameraAniPref.gameObject);
        Destroy(cameraShakeAniPref.gameObject);
    }
    public Animator AddCameraInfo(string name,Animator pref)
    {
        Animator ani = Instantiate(pref, lastCamEffect);
        lastCamEffect = ani.transform;
        ani.name = name;
        return ani;
    }
    public Animator GetCameraInfo(string name, bool shake)
    {
        Animator ani = null;
        if (shake)
        {
            foreach (Animator cam in cameraShakeAni)
                if (cam.name == name)
                    ani = cam;
        }
        else
            foreach (Animator cam in cameraAni)
                if (cam.name == name)
                    ani = cam;
        return ani;
    }

    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad >= 1.2f & !playedSFX)
        {
            playedSFX = true;
            if (startSFX)
                FXManager.SpawnSFX(startSFX, transform.position, 10, 8, 0.6f);
        }
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
        if (Time.fixedTime < 1)
        {
            lookHorizontal = 0;
            lookVertical = 0;
        }

        // Mouse rotation
        rotY += lookVertical * -GetLookSpeed() * Time.fixedDeltaTime / 2;
        rotY = Mathf.Clamp(rotY, lookDownLimit, lookUpLimit);

        // Camera rotation
        if (active)
            Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, Quaternion.Euler(rotY, 0f, 0f), GetLookSmoothing(false));

        if (jumping == 0 & landedTime == 0 & grounded & active)
        {
            // Move the GameObject
            rigi.AddForce(movement * GetMoveSpeed());
            bool movingUp = false;
            if (movementInput.y > 0 & CheckVault(transform.TransformDirection(Vector3.forward), 8))
                movingUp = true;
            if (movementInput.y < 0 & CheckVault(-transform.TransformDirection(Vector3.forward), 8))
                movingUp = true;
            if (movementInput.x > 0 & CheckVault(transform.TransformDirection(Vector3.right), 7))
                movingUp = true;
            if (movementInput.x < 0 & CheckVault(-transform.TransformDirection(Vector3.right), 7))
                movingUp = true;
            if (movingUp)
                rigi.AddForce(transform.up * vaultForce);

            // Checks if moving for the camera bob
            if (movement.magnitude > 0)
            {
                if (Time.time >= lastWalkTime + walkFrequency)
                {
                    lastWalkTime = Time.time;
                    if (walkingSFX.Length > 0)
                        walkingSources[currentFoot].PlayOneShot(walkingSFX[Random.Range(0, walkingSFX.Length - 1)]);
                    //  FXManager.SpawnSFX(walkingSFX[Random.Range(0, walkingSFX.Length - 1)], transform.position, 50, 5);
                    if (!stunned)
                        GetCameraInfo("Walk", true).CrossFadeInFixedTime("Walk", 0.1f);
                    if (walksVFX.Length > 0)
                        FXManager.SpawnVFX(walksVFX[Random.Range(0, walksVFX.Length - 1)], feet[currentFoot].transform.position, new Vector3(-90, 0, 0), null, 5, false, 0.7f);
                    if (currentFoot == 0)
                        currentFoot = 1;
                    else
                        currentFoot = 0;
                }
                StartHeadbob();
            }

            // Mouse rotation
            rotX += lookHorizontal * GetLookSpeed() * Time.fixedDeltaTime;
            if (lookHorizontal != 0)
                looking = true;

            // Camera rotation
            if (active)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, rotX, 0f).normalized, GetLookSmoothing(true));
        }
        else
            lastWalkTime = Time.time;
        if (looking)
        {
            if (Time.fixedTime >= lastLookTime + lookFrequency)
            {
                lastLookTime = Time.fixedTime;
                if (lookSFX.Length > 0)
                    FXManager.SpawnSFX(lookSFX[Random.Range(0, lookSFX.Length - 1)], transform.position, 10, 5, 0.1f);
            }
        }
        else
            lastLookTime = 0;
        StopHeadbob();
    }

    void Update()
    {
        // This checks if on the ground Camera.main.transform.parent
        bool air = false;
        if (feetScript.ground.Count > 0)
        {
            grounded = true;
            if (jumping == 0)
                air = true;
            lightningStruckFall = false;
            if (wasGrounded)
                airTime = Time.time;
        }
        else
        {
            // This determines how long you are in the air for for fall damage or a bigger landing effect
            grounded = false;
        }
        if (air)
        {
            rigi.mass = mass;
            rigi.drag = drag;
        }
        else
        {
            rigi.mass = playerFallMass;
            rigi.drag = playerFallDrag;
        }
        if (GameManager.me)
            if (transform.position.y <= killHeight & !GameManager.me.over)
                GameManager.me.FailObjective(true);

        if (!startLand & grounded)
            StartCoroutine(SetUp());

        //   input.defaultActionMap = "Keyboard";
        //  input.defaultActionMap = "JoystickController";

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
                bool p = false;
                if (!stunned & !jumpAni)
                {
                    lightningStruckFall = false;
                    GetCameraInfo("Land", true).CrossFadeInFixedTime("Land", 0.1f);
                    jumpAni = true;
                    p = true;
                    landedTime = Time.time;
                    // For Squishing Bugs
                    foreach (Health enemyHealth in FindObjectsOfType<Health>())
                        if (enemyHealth.gameObject != gameObject & Vector3.Distance(transform.position, enemyHealth.transform.position) <= landingDamageAreaEffect)
                            enemyHealth.Damage((int)landingDamageAreaEffect, DamageType.Physical);
                    if (landingSFX.Length > 0)
                        wasteSource.PlayOneShot(landingSFX[Random.Range(0, landingSFX.Length - 1)]);
                    //   FXManager.SpawnSFX(landingSFX[Random.Range(0, landingSFX.Length - 1)], transform.position, 100, 5);
                    if (landingVFX)
                        FXManager.SpawnVFX(landingVFX, feetScript.transform.position, transform.eulerAngles, null, 15);
                }
                else if (!stunnedAni)
                {
                    GetCameraInfo("Lightning", true).CrossFadeInFixedTime("Lightning", 0.1f);
                    stunnedAni = true;
                    p = true;
                    landedTime = Time.time + stunTime;
                }
                if (p)
                {
                    string landAni = "Land";
                    if (Time.time >= airTime + landingForces[1])
                    {
                        landedTime = Time.time;
                        landAni = "Land";
                    }
                    GetCameraInfo("Land", false).CrossFadeInFixedTime(landAni, 0.1f);
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
            stunnedAni = false;
            dangerLightning = false;
            stunTime = 0;
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

    public float GetMoveSpeed()
    {
        float speed = moveSpeedKeyboard;
        if (input.defaultActionMap == "Keyboard")
            speed = moveSpeedKeyboard;
        if (input.defaultActionMap == "JoystickController")
            speed = moveSpeedJoystick;
        return speed;
    }
    public float GetLookSpeed()
    {
        float speed = lookSpeedKeyboard;
        if (input.defaultActionMap == "Keyboard")
            speed = lookSpeedKeyboard;
        if (input.defaultActionMap == "JoystickController")
            speed = lookSpeedJoystick;
        return speed;
    }

    public float GetLookSmoothing(bool x)
    {
        float speed = 0;
        if (input.defaultActionMap == "Keyboard")
            if (x)
                speed = lookSmoothingXKeyboard;
            else
                speed = lookSmoothingYKeyboard;

        if (input.defaultActionMap == "JoystickController")
            if (x)
                speed = lookSmoothingXJoystick;
            else
                speed = lookSmoothingYJoystick;
        return speed;
    }

    public bool CheckVault(Vector3 direction, float range)
    {
        bool bottom = Physics.Raycast(vaultPos[0].transform.position, direction, range);
        bool top = Physics.Raycast(vaultPos[1].transform.position, direction, range + 9);
        return bottom & !top;
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

    public void Stun(float time = 2.3f, bool lightning = false)
    {
        stunned = true;
        if (lightning)
            dangerLightning = true;
        stunTime = time;
        lastStunTime = Time.time;
        lightningStruckFall = true;
        if (downSFX.Length > 0)
            FXManager.SpawnSFX(downSFX[Random.Range(0, downSFX.Length - 1)], transform.position, 100, 5, 0.3f);
    }

    public void FireWeapon()
    {
        GetCameraInfo("Cannon", true).CrossFadeInFixedTime("Cannon", 0.1f);
    }
    public void FireMiningLazer(bool hit)
    {
        if (hit)
            GetCameraInfo("Mining_Lazer_Hit", true).CrossFadeInFixedTime("Mining_Lazer_Hit", 0.1f);
        else
            GetCameraInfo("Mining_Lazer", true).CrossFadeInFixedTime("Mining_Lazer", 0.1f);
    }

    public void Damage()
    {
        GetCameraInfo("Damage", true).CrossFadeInFixedTime("Damage", 0.1f);
        if (downSFX.Length > 0)
            FXManager.SpawnSFX(downSFX[Random.Range(0, downSFX.Length - 1)], transform.position, 100, 5, 0.3f);
    }

    public void Die()
    {
        active = false;
        GetCameraInfo("Death", true).CrossFadeInFixedTime("Death", 0.1f);
        if (downSFX.Length > 0)
            FXManager.SpawnSFX(downSFX[Random.Range(0, downSFX.Length - 1)], transform.position, 100, 5, 0.3f);
        if (GameManager.me)
            GameManager.me.FailObjective(true);
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

    public void OnAbort(InputAction.CallbackContext context)
    {
        if (context.started)
            GameManager.me.aborting = true;
        else if (context.canceled)
            GameManager.me.aborting = false;
    }

    // Make sure you call a StartCoroutine instead of a regular void
    public IEnumerator WaitJump()
    {
        jumping = 1;
        rigi.mass = playerFallMass;
        GetCameraInfo("Jump", false).CrossFadeInFixedTime("Jump", 0.1f);
        yield return new WaitForSeconds(jumpSpeed);
        rigi.AddForce(Vector3.up * jumpHeight);
        rigi.AddForce(Camera.main.transform.forward * jumpForward);
        lightningStruckFall = false;
        if (jumpingSFX.Length > 0)
            wasteSource.PlayOneShot(jumpingSFX[Random.Range(0, jumpingSFX.Length - 1)]);
         //   FXManager.SpawnSFX(jumpingSFX[Random.Range(0, jumpingSFX.Length - 1)], transform.position, 50, 5);
        jumping = 0;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Get look input from joystick
        lookInput = context.ReadValue<Vector2>();
    }
}

