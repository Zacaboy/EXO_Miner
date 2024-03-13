using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to inflict

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has a Health component
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            // Inflict damage on the object's health
            health.TakeDamage(damageAmount);
        }
    }
}
