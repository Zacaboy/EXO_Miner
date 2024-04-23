using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour
{
    public int damage = 1;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        health.maxHealth = health.maxHealth - damage;
    }
}
