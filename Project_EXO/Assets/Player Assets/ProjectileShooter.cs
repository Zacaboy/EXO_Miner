using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public Transform firePoint; // Point from which the projectile will be fired
    public float shootForce = 10f; // Force at which the projectile is shot
    public float destroyDelay = 3f; // Time after which the projectile will be destroyed

    private bool isShooting; // Flag to track if the player is shooting

    void Update()
    {
        // Check for shooting input
        if (isShooting)
        {
            Shoot();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        // Check if the action is triggered
        if (context.started)
        {
            // Set isShooting flag to true
            isShooting = true;
        }
        // Check if the action is canceled (released)
        else if (context.canceled)
        {
            // Set isShooting flag to false
            isShooting = false;
        }
    }

    void Shoot()
    {
        // Instantiate a new projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        // Add force to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }

        // Destroy the projectile after a delay
        Destroy(projectile, destroyDelay);
    }
}