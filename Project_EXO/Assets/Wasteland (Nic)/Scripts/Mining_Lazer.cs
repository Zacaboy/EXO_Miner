using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining_Lazer : MonoBehaviour
{
    public float range = 18;
    public float mineRate = 0.1f;
    public float mineDamage = 25;
    public bool isFiring;
    public Renderer lazerR;
    public Transform muzzle;
    public Transform pivot;
    public ParticleSystem lazerVFX;
    [HideInInspector] public AudioSource lazerSFX;
    Transform lookpos;
    [HideInInspector] public Transform currentObject;
    [HideInInspector] public Ore currentOre;
    [HideInInspector] public float lastMineTime;

    // Start is called before the first frame update
    void Start()
    {
        lazerSFX = GetComponent<AudioSource>();
        lookpos = new GameObject().transform;
        lookpos.transform.position = Camera.main.transform.position;
        lookpos.transform.eulerAngles = Camera.main.transform.eulerAngles;
        lookpos.transform.position += lookpos.forward * 15;
        lookpos.SetParent(transform);
        lookpos.name = "Look Pos";
        pivot.localEulerAngles = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartFire();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            StopFire();
        muzzle.transform.LookAt(lookpos.position);
        lazerR.gameObject.SetActive(isFiring);
        if (isFiring)
        {
            lazerSFX.Play();
            if(lazerVFX)
                lazerVFX.Play();
            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, muzzle.TransformDirection(Vector3.forward), out hit, range))
                currentObject = hit.transform;
            else
                currentObject = null;
            if (currentObject)
                currentOre = currentObject.GetComponent<Ore>();
            else
                currentOre = null;
            if (currentOre)
            {
                if (Time.time >= lastMineTime + mineRate)
                {
                    lastMineTime = Time.time;
                    currentOre.Mine(mineDamage);
                }
            }
        }
        else
        {
            lazerSFX.Stop();
            if (lazerVFX)
                lazerVFX.Stop();
            currentObject = null;
            currentOre = null;
        }
    }

    public void StartFire()
    {
        isFiring = true;

    }

    public void StopFire()
    {
        isFiring = false;
    }
}
