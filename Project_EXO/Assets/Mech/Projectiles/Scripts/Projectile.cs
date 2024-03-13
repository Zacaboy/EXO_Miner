using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem vfx;
    public AudioClip sfx;
    float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Time.fixedTime < spawnTime + 0.01f) return;
        FXManager.SpawnVFX(vfx, collision.contacts[0].point, collision.contacts[0].point, 5f);
        FXManager.SpawnSFX(sfx, collision.contacts[0].point, 400, 5f);
        Destroy(gameObject);
    }
}
