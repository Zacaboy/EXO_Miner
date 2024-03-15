using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float maxHealth = 100;
    public string resourceType;
    public ParticleSystem mineVFX;
    public AudioClip mineSFX;
    public ParticleSystem destroyVFX;
    public AudioClip destroySFX;
    public Sprite icon;
    [HideInInspector] public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Mine(float damage)
    {
        if (health <= 0)
            return;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        // Mining code
        if (health <= 0)
        {
            if (destroySFX)
                FXManager.SpawnSFX(destroySFX, transform.position, 200, 3);
            if (destroyVFX)
                FXManager.SpawnVFX(destroyVFX, transform.position, transform.position, 5, true);
            if (Mining_UI.me)
                Mining_UI.me.healthAmount = 0;
            if (ResourceTracker.me)
                ResourceTracker.me.AddResource(resourceType);
            Destroy(gameObject);
        }
        else
        {
            if (mineSFX)
                FXManager.SpawnSFX(mineSFX, transform.position, 200, 3);
            if (mineVFX)
                FXManager.SpawnVFX(mineVFX, transform.position, transform.position, 5, true);
        }
    }
}
