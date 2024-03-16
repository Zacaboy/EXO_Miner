using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnEffect : MonoBehaviour {

    public float spawnEffectTime = 2;
    public float pause = 1;
    public float fadeDelay = 1;
    public AnimationCurve fadeIn;
    public bool useAlpha = true;

    ParticleSystem ps;
    float timer = 0;
    Renderer _renderer;

    int shaderProperty;
    float lastTime;

	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_Cutoff");
        _renderer = GetComponent<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();
        lastTime = Time.time;
        if (ps)
        {
            var main = ps.main;
            main.duration = spawnEffectTime;
            ps.Play();
        }
    }
	
	void Update ()
    {
        if (timer < spawnEffectTime + pause & Time.time >= lastTime + fadeDelay)
            timer += Time.deltaTime;
        else
        {
            if (ps)
                ps.Play();
            timer = 0;
        }
        if (timer >= 3)
            GetComponent<Collider>().enabled = false;
        if (_renderer.material.GetColor("_BaseColor").a <= 0.3f)
            Destroy(gameObject);
        if (useAlpha)
            _renderer.material.SetColor("_BaseColor", Color.Lerp(_renderer.material.GetColor("_BaseColor"), new Color(_renderer.material.GetColor("_BaseColor").r, _renderer.material.GetColor("_BaseColor").g, _renderer.material.GetColor("_BaseColor").b, 0), fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer))));
        else
            _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer)));
    }
}
