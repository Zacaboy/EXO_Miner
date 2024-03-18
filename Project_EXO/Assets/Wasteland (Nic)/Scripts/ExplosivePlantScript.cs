using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePlantScript : MonoBehaviour
{
    public float distance = 10;
    public float explodeTime = 3;
    public float explodeRadius = 70;
    public float imissiveIntensity = 2;
    public Color explodeColour = Color.red;
    public ParticleSystem explodeVFX;
    public AudioClip explodeSFX;
    AudioSource source;
    [HideInInspector] public Health health;
    [HideInInspector] public Renderer m_Renderer;
    Color normalColour;
    Light m_Light;
    float startIntensity;
    float lastTime;
    bool close;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        m_Renderer = GetComponentInChildren<Renderer>();
        m_Light = GetComponentInChildren<Light>();
        startIntensity = m_Light.intensity;
        m_Light.intensity = 0;
        normalColour = m_Renderer.material.GetColor("_EmissionColor");
        health.deathEvent.AddListener(Explode);
        if (GetComponentInChildren<Collider>())
        {
            if (!GetComponentInChildren<Collider>().enabled)
                close = true;
        }
        else
            close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (close)
        {
            if (PlayerMechController.me)
            {
                if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) <= distance & !health.dead)
                {
                    if (!source.isPlaying)
                        source.Play();
                    m_Renderer.material.SetColor("_EmissionColor", Color.Lerp(m_Renderer.material.GetColor("_EmissionColor") * imissiveIntensity, explodeColour, 0.1f));
                    m_Light.intensity = Mathf.Lerp(m_Light.intensity, startIntensity, 0.1f);
                    if (Time.time >= lastTime + explodeTime)
                        health.Damage(1000);
                }
                else
                {
                    lastTime = Time.time;
                    if (source.isPlaying)
                        source.Stop();
                    m_Renderer.material.SetColor("_EmissionColor", Color.Lerp(m_Renderer.material.GetColor("_EmissionColor"), normalColour * 1, 0.1f));
                    m_Light.intensity = Mathf.Lerp(m_Light.intensity, 0, 0.1f);

                }
                ExcludeGround();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMechController>() & !health.dead)
        {
            lastTime = Time.time;
            close = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlayerMechController>() & !health.dead)
            close = false;
        ExcludeGround();
    }

    public void Explode()
    {
        if (explodeVFX)
            FXManager.SpawnVFX(explodeVFX, transform.position, transform.eulerAngles, null, 7);
        if (explodeSFX)
            FXManager.SpawnSFX(explodeSFX, transform.position, 60, 7);
        if (PlayerMechController.me)
            if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) <= explodeRadius)
                PlayerMechController.me.Stun(true);
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        ExcludeGround();
    }

    public void ExcludeGround()
    {
        if (PlayerMechController.me)
            if (PlayerMechController.me.GetComponentInChildren<FeetScript>().ground.Contains(transform))
                PlayerMechController.me.GetComponentInChildren<FeetScript>().ground.Remove(transform);
    }
}
