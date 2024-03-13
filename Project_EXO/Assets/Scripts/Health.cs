using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health points
    public int currentHealth; // Current health points

    // Event delegate for health change
    public delegate void HealthChangedDelegate(int currentHealth, int maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        // Reduce current health by damage amount
        currentHealth -= damageAmount;

        // Ensure current health doesn't go below 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Invoke the OnHealthChanged event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Check if health has reached 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal
    public void Heal(int healAmount)
    {
        // Increase current health by heal amount
        currentHealth += healAmount;

        // Ensure current health doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        // Invoke the OnHealthChanged event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Method to handle death
    void Die()
    {
        // Destroy the GameObject
        Destroy(gameObject);
    }
}
