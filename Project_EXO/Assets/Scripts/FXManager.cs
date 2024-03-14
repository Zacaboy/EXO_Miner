using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static void SpawnSFX(AudioClip sfx, Vector3 pos, float distance, float lifeTime, float volume = 1)
    {
        AudioSource newSFX = new GameObject().AddComponent<AudioSource>();
        newSFX.clip = sfx;
        newSFX.spatialBlend = 1;
        newSFX.rolloffMode = AudioRolloffMode.Linear;
        newSFX.maxDistance = distance;
        newSFX.transform.position = pos;
        newSFX.name = sfx.name + " SFX";
        newSFX.volume = volume;
        newSFX.Play();
        Destroy(newSFX.gameObject, lifeTime);
    }

    public static void SpawnVFX(ParticleSystem vfx, Vector3 pos, Vector3 rot, float lifeTime, bool facePlayer = false)
    {
        ParticleSystem newVFX = Instantiate(vfx);
        newVFX.transform.position = pos;
        newVFX.transform.eulerAngles = rot;
        if(facePlayer)
        {
            Transform look = new GameObject().transform;
            look.position = pos;
            look.LookAt(Camera.main.transform.position);
            newVFX.name = vfx.name + " VFX";
            newVFX.transform.eulerAngles = look.eulerAngles;
            Destroy(look.gameObject);
        }
        newVFX.Play();
        Destroy(newVFX.gameObject, lifeTime);
    }
}
