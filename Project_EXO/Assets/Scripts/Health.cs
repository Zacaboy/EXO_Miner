using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DamageType { Lead, Physical, Lazer }

public class Health : MonoBehaviour
{
    public int maxHealth = 25;
    public bool immuneToLazer = true;
    public string objectiveName;
    [HideInInspector] public Objective objective;
    [HideInInspector] public int health;
    [HideInInspector] public bool dead;
    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (objectiveName != "")
            objective = ObjectiveManager.me.FindObjective(objectiveName);
    }

    public void Damage(int damage, DamageType type)
    {
        if (!dead)
        {
            if (type == DamageType.Lazer & immuneToLazer) return;
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health <= 0)
                Death();
            else
                damageEvent.Invoke();
        }
    }

    public void Death()
    {
        if (dead) return;
        if (ObjectiveManager.me & objective.name != "")
        {
            if (objective.destroyTargets)
                ObjectiveManager.me.UpdateProgress(objectiveName, 1);
            if (ObjectiveManager.me.currentObjective.targets.Contains(transform))
                ObjectiveManager.me.currentObjective.targets.Remove(transform);
        }
        dead = true;
        deathEvent.Invoke();
    }
}
