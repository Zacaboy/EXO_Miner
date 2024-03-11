using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float maxHealth = 100;
    public ParticleSystem mineVFX;
    public AudioClip mineSFX;
    public ParticleSystem destroyVFX;
    public AudioClip destroySFX;
    AudioSource SFX;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        SFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnVFX(ParticleSystem vfx, Vector3 pos, float lifeTime)
    {
        ParticleSystem newVFX = Instantiate(vfx);
        newVFX.transform.position = pos;
        newVFX.Play();
        Destroy(newVFX.gameObject, lifeTime);
    }

    public void Mine(float damage)
    {
        if (health <= 0)
            return;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        // Put code for mining here
        if (health <= 0)
        {
            SFX.PlayOneShot(destroySFX);
            if (destroyVFX)
                SpawnVFX(destroyVFX, transform.position, 5);
            if (Mining_UI.me)
                Mining_UI.me.healthAmount = 0;
            Destroy(gameObject);
        }
        else
        {
            SFX.PlayOneShot(mineSFX);
            if (mineVFX)
                SpawnVFX(mineVFX, transform.position, 5);
            if (Mining_UI.me)
                if (Mining_UI.me.healthAmount == 0)
                {
                    Mining_UI.me.healthAmount = health / maxHealth;
                    Mining_UI.me.health.fillAmount = Mining_UI.me.healthAmount;
                    Mining_UI.me.healthAfter.fillAmount = Mining_UI.me.healthAmount;
                }
        }
    }
}
