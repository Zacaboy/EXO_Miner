using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float maxHealth = 100;
    public string resourceType;
    public int amount = 1;
    public ParticleSystem mineVFX;
    public AudioClip mineSFX;
    public ParticleSystem destroyVFX;
    public AudioClip destroySFX;
    public Sprite icon;
    [HideInInspector] public float health;

    [Header("Effect")]
    public float spawnEffectTime = 2;
    public float pause = 1;
    public float fadeDelay = 1;
    public bool useAlpha = true;
    public AnimationCurve fadeIn;
    public ParticleSystem destroyFadeVFX;

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
            SpawnEffect effect = gameObject.AddComponent<SpawnEffect>();
            effect.spawnEffectTime = spawnEffectTime;
            effect.pause = pause;
            effect.fadeIn = fadeIn;
            effect.useAlpha = useAlpha;
            effect.fadeDelay = fadeDelay;
            if (destroyFadeVFX)
                FXManager.SpawnVFX(destroyFadeVFX, transform.position, new Vector3(), null, 8, false);
            if (destroySFX)
                FXManager.SpawnSFX(destroySFX, transform.position, 200, 3);
            if (destroyVFX)
                FXManager.SpawnVFX(destroyVFX, transform.position, transform.position, null, 5, true);
            if (Mining_UI.me)
                Mining_UI.me.healthAmount = 0;
            if (ResourceTracker.me)
                ResourceTracker.me.AddResource(resourceType, amount);
            Destroy(gameObject, 5);
        }
        else
        {
            if (mineSFX)
                FXManager.SpawnSFX(mineSFX, transform.position, 200, 3);
            if (mineVFX)
                FXManager.SpawnVFX(mineVFX, transform.position, transform.position, null, 5, true);
        }
    }
}
