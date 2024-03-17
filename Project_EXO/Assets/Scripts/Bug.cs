using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [HideInInspector] public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.damageEvent.AddListener(Damage);
        health.deathEvent.AddListener(Kill);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {

    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
