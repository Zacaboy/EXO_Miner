using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mining_Lazer : MonoBehaviour
{
    public float range = 18;
    public float mineRate = 0.1f;
    public float mineDamage = 25;
    public float lazerInSpeed = 0.4f;
    public float lazerOutSpeed = 0.5f;
    public float heatChangeRate = 0.5f;
    public Color[] lazerColours;
    [HideInInspector] public bool isFiring;
    public bool showDebug;
    public Renderer lazerR;
    public Transform muzzle;
    public Transform pivot;
    public ParticleSystem lazerVFX;
    public ParticleSystem lazerHitVFX;
    public AudioClip lazerHitSFX;
    [HideInInspector] public AudioSource lazerSFX;
    Light[] m_Lights;
    [HideInInspector] public Transform currentObject;
    [HideInInspector] public Ore currentOre;
    [HideInInspector] public float lastMineTime;
    public int heat;
    float lastHeatChange;

    // Start is called before the first frame update
    void Start()
    {
        lazerSFX = GetComponent<AudioSource>();
        pivot.localEulerAngles = new Vector3(90, 0, 0);
        m_Lights = GetComponentsInChildren<Light>();
        for (int i = 0; i < m_Lights.Length; i++)
            m_Lights[i].intensity = 0;
        lazerR.transform.parent.localScale = new Vector3(0, 0, 0);
        lazerColours[0] = lazerR.material.GetColor("_EmissionColor");
    }

    void FixedUpdate()
    {
        muzzle.transform.LookAt(PlayerMechController.me.lookpos.position);
        lazerR.gameObject.SetActive(isFiring);
        lazerR.material.SetColor("_EmissionColor", Color.Lerp(lazerR.material.GetColor("_EmissionColor"), lazerColours[heat], 0.01f));
        for (int i = 0; i < m_Lights.Length; i++)
        {
            m_Lights[i].color = Color.Lerp(m_Lights[i].color, lazerColours[heat], 0.01f);
            if (isFiring)
                m_Lights[i].intensity = Mathf.Lerp(m_Lights[i].intensity, 100, 0.01f);
            else
                m_Lights[i].intensity = Mathf.Lerp(m_Lights[i].intensity, 0, 0.01f);
        }
        if (Time.fixedTime >= lastHeatChange + heatChangeRate)
        {
            lastHeatChange = Time.fixedTime;
            if (isFiring)
                heat += 1;
            else
                heat -= 1;
            heat = Mathf.Clamp(heat, 0, lazerColours.Length - 1);
        }
        if (isFiring)
        {
            lazerSFX.Play();
            if (lazerVFX)
                lazerVFX.Play();
            lazerR.transform.parent.localScale = Vector3.Lerp(lazerR.transform.parent.localScale, new Vector3(1, 1, 1), lazerInSpeed);
            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, range))
                currentObject = hit.transform;
            else
                currentObject = null;
            if (currentObject)
            {
                if (showDebug)
                    Debug.DrawRay(muzzle.position, muzzle.TransformDirection(Vector3.forward) * range, Color.green, 1);
                currentOre = currentObject.GetComponent<Ore>();
            }
            else
            {
                if (showDebug)
                    Debug.DrawRay(muzzle.position, muzzle.TransformDirection(Vector3.forward) * range, Color.red, 1);
                currentOre = null;
            }
            if (currentObject)
            {
                if (lazerHitVFX)
                    FXManager.SpawnVFX(lazerHitVFX, hit.point, hit.point, 5, true);
                if (lazerHitSFX)
                    FXManager.SpawnSFX(lazerHitSFX, hit.point, 200, 5);
            }
            if (currentOre & lazerR.transform.parent.localScale.magnitude >= 0.9f)
            {
                lazerR.material.color = Color.red;
                if (currentOre.mineVFX)
                    FXManager.SpawnVFX(currentOre.mineVFX, hit.point, hit.point, 5, true);
                if (Time.fixedTime >= lastMineTime + mineRate)
                {
                    lastMineTime = Time.fixedTime;
                    currentOre.Mine(mineDamage);
                }
            }
        }
        else
        {
            lazerSFX.Stop();
            if (lazerVFX)
                lazerVFX.Stop();
            lazerR.transform.parent.localScale = Vector3.Lerp(lazerR.transform.parent.localScale, new Vector3(0, 0, 0), lazerOutSpeed);
            currentObject = null;
            currentOre = null;
        }
    }

    public void OnTrigger(InputAction.CallbackContext context)
    {
        // Check if the action is triggered
        if (context.started)
            StartFire();
        if(context.canceled)
            StopFire();
    }

    public void StartFire()
    {
        isFiring = true;
    }
    public void StopFire()
    {
        isFiring = false;
    }
}
