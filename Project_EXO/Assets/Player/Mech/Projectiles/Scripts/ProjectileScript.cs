using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public int damage = 15;
    public int areaOfEffect = 6;
    public DamageType type;
    public ParticleSystem vfx;
    public AudioClip sfx;
    [HideInInspector] public GameObject shooter;
    float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Time.fixedTime < spawnTime + 0.01f) return;
        FXManager.SpawnVFX(vfx, collision.contacts[0].point, collision.contacts[0].point, null, 5f);
        FXManager.SpawnSFX(sfx, collision.contacts[0].point, 400, 5f);
        Health health = collision.transform.GetComponentInParent<Health>();
        if (health)
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
