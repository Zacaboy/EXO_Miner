using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    public float spawnEffectTime = 2;
    public float pause = 1;
    public float fadeDelay = 1;
    public AnimationCurve fadeIn;
    public bool useAlpha = true;
    public bool fadingIn;
    public string property = "_BaseColor";

    ParticleSystem ps;
    float timer = 0;
    Renderer _renderer;

    int shaderProperty;
    float lastTime;

	void Start ()
    {
        Initiate();
    }

    public void Initiate()
    {
        shaderProperty = Shader.PropertyToID("_Cutoff");
        _renderer = GetComponentInChildren<Renderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        lastTime = Time.time;
        if (useAlpha & fadingIn)
            _renderer.material.SetColor(property, new Color(_renderer.material.GetColor(property).r, _renderer.material.GetColor(property).g, _renderer.material.GetColor(property).b, 0));
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
            if (GetComponent<Collider>())
                GetComponent<Collider>().enabled = false;
        if (useAlpha)
        {
            if (fadingIn)
                _renderer.material.SetColor(property, Color.Lerp(_renderer.material.GetColor(property), new Color(_renderer.material.GetColor(property).r, _renderer.material.GetColor(property).g, _renderer.material.GetColor(property).b, 1), fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer))));
            else
                _renderer.material.SetColor(property, Color.Lerp(_renderer.material.GetColor(property), new Color(_renderer.material.GetColor(property).r, _renderer.material.GetColor(property).g, _renderer.material.GetColor(property).b, 0), fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer))));
            if (fadingIn)
            {
                if (_renderer.material.GetColor(property).a >= 1)
                    Destroy(this);
            }
            else if (_renderer.material.GetColor(property).a <= 0.3f)
                    Destroy(gameObject);       
        }
        else
        {
            _renderer.material.SetFloat("_Cutoff", fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer)));
            if (fadingIn)
            {
                if (_renderer.material.GetColor(property).a >= 1)
                    Destroy(this);
            }
            else if (_renderer.material.GetFloat("_Cutoff") <= 0.3f)
                Destroy(gameObject);
        }
    }
}
