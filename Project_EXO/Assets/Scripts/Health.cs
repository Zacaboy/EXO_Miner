using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 25;
    public int health;
    public bool dead;
    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        if (!dead)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health <= 0)
            {
                dead = true;
                deathEvent.Invoke();
            }
            else
                damageEvent.Invoke();
        }
    }
}
