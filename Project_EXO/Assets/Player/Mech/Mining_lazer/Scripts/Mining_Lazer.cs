using System.Collections.Generic;
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
    public float lazerSFXRate = 0.49f;
    public float mineSFXRate = 0.18f;
    public Color[] lazerColours;
    [HideInInspector] public bool isFiring;
    public bool showDebug;
    public Renderer lazerR;
    public Transform muzzle;
    public Transform pivot;
    public ParticleSystem lazerVFX;
    public ParticleSystem lazerHitVFX;
    public Transform decal;
    public AudioClip lazerSFX;
    public AudioClip lazerHitSFX;
    public AudioClip lazerStopSFX;
    public Light triggerLight;
    List<Light> m_Lights = new List<Light>();
    [HideInInspector] public Transform currentObject;
    [HideInInspector] public Ore currentOre;
    [HideInInspector] public float lastMineTime;
    public int heat;
    float lastHeatChange;
    float lastMineSFX;
    float lastHitSFX;
    float startMuzzle;
    float fireTime;

    // Start is called before the first frame update
    void Start()
    {
        pivot.localEulerAngles = new Vector3(90, 0, 0);
        foreach (Light light in GetComponentsInChildren<Light>())
            if (light != triggerLight)
                m_Lights.Add(light);
        for (int i = 0; i < m_Lights.Count; i++)
            m_Lights[i].intensity = 0;
        lazerR.transform.parent.localScale = new Vector3(0, 0, 0);
        lazerColours[0] = lazerR.material.GetColor("_EmissionColor");
        startMuzzle = triggerLight.intensity;
        triggerLight.intensity = 0;
    }

    void FixedUpdate()
    {
        muzzle.transform.LookAt(PlayerMechController.me.lookpos.position + PlayerMechController.me.lookpos.right * 2);
        lazerR.gameObject.SetActive(isFiring);
        for (int i = 0; i < lazerR.materials.Length; i++)
            lazerR.materials[i].SetColor("_EmissionColor", Color.Lerp(lazerR.materials[i].GetColor("_EmissionColor"), lazerColours[heat], 0.01f));
        for (int i = 0; i < m_Lights.Count; i++)
        {
            m_Lights[i].color = Color.Lerp(m_Lights[i].color, lazerColours[heat], 0.01f);
            if (isFiring)
                m_Lights[i].intensity = Mathf.Lerp(m_Lights[i].intensity, 100, 0.01f);
            else
                m_Lights[i].intensity = Mathf.Lerp(m_Lights[i].intensity, 0, 0.08f);
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
            if (lazerVFX)
                lazerVFX.Play();
            int shake = 0;
            if (Time.fixedTime >= lastMineSFX + lazerSFXRate)
            {
                lastMineSFX = Time.fixedTime;
                shake = 1;
                if (lazerSFX)
                    FXManager.SpawnSFX(lazerSFX, muzzle.position, 100, 3);
            }
            if (Time.fixedTime < fireTime + 0.15f)
                triggerLight.intensity = Mathf.Lerp(triggerLight.intensity, startMuzzle, 0.5f);
            else
                triggerLight.intensity = Mathf.Lerp(triggerLight.intensity, 0, 0.2f);
            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, range))
            {
                currentObject = hit.transform;
                lazerR.transform.parent.localScale = Vector3.Lerp(lazerR.transform.parent.localScale, new Vector3(1, hit.distance, 1), lazerInSpeed);
                if (lazerR.transform.parent.localScale.y > hit.distance)
                    lazerR.transform.parent.localScale = new Vector3(1, hit.distance, 1);
                if (decal)
                {
                    Transform newDecal = Instantiate(decal);
                    newDecal.position = hit.point + hit.normal * 0.0001f;
                    newDecal.rotation = Quaternion.LookRotation(-hit.normal);
                    newDecal.SetParent(hit.transform);
                    Destroy(newDecal.gameObject, 15);
                }
            }
            else
            {
                currentObject = null;
                lazerR.transform.parent.localScale = Vector3.Lerp(lazerR.transform.parent.localScale, new Vector3(1, range, 1), lazerInSpeed);
            }
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
                if(Time.fixedTime >= lastHitSFX + mineSFXRate)
                {
                    shake = 2;
                    lastHitSFX = Time.fixedTime;
                    if (lazerHitVFX)
                        FXManager.SpawnVFX(lazerHitVFX, hit.point, hit.point, 5, true);
                    if (lazerHitSFX)
                        FXManager.SpawnSFX(lazerHitSFX, hit.point, 100, 5);
                }
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
            if(shake > 0)
                GetComponentInParent<PlayerMechController>().FireMiningLazer(shake == 2);
        }
        else
        {
            if (lazerVFX)
                lazerVFX.Stop();
            triggerLight.intensity = Mathf.Lerp(triggerLight.intensity, 0, 0.2f);
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
        if (context.canceled)
            StopFire();
    }

    public void StartFire()
    {
        if (!isFiring)
            fireTime = Time.fixedTime;
        isFiring = true;
    }
    public void StopFire()
    {
        isFiring = false;
        if (lazerStopSFX)
            FXManager.SpawnSFX(lazerStopSFX, muzzle.position, 100, 5);
    }
}
