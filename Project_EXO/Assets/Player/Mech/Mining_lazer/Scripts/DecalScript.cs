using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalScript : MonoBehaviour
{
    public float lifeTime = 2;
    public string property = "_Color";
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= spawnTime + lifeTime)
        {
            SpawnEffect effect = gameObject.AddComponent<SpawnEffect>();
            effect.spawnEffectTime = 4;
            effect.pause = 3;
            effect.fadeIn = GameManager.me.fadeIn;
            effect.useAlpha = true;
            effect.property = property;
            effect.fadeDelay = 2;
            Destroy(this);
        }
    }
}
