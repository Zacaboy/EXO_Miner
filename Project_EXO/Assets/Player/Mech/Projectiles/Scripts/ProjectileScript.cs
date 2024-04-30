using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ProjectileScript : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 15;
    public int areaOfEffect = 6;
    public DamageType type;

    [Header("Effects")]
    public ParticleSystem vfx;
    public AudioClip sfx;

    [Header("Ignore")]
    [HideInInspector] public GameObject shooter;
    AudioSource source;
    float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
        source = GetComponent<AudioSource>();
        Debug.Log(source);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Time.fixedTime < spawnTime + 0.01f) return;
        FXManager.SpawnVFX(vfx, collision.contacts[0].point, collision.contacts[0].point, null, 5f);
        source.PlayOneShot(sfx);
        Health health = collision.transform.GetComponentInParent<Health>();
        if (health)
            if (health.gameObject != shooter)
                health.Damage(damage, type);
        foreach (Health enemyHealth in FindObjectsOfType<Health>())
        {
            bool hit = true;
            if (health)
                if (enemyHealth.gameObject == health.gameObject)
                    hit = false;
            if (enemyHealth.gameObject != shooter & Vector3.Distance(transform.position, enemyHealth.transform.position) <= areaOfEffect)
                if (hit)
                    enemyHealth.Damage(damage, type);
        }
        Destroy(gameObject);
    }
}
