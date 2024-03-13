using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatlingMinigun : MonoBehaviour
{
    public Transform[] barrelTransforms;   // Transforms of the barrels
    public GameObject projectilePrefab;    // Prefab of the projectile to shoot
    public float fireRate = 10f;           // Rate of fire (projectiles per second)
    public float shootForce = 10f;         // Force at which the projectile is shot
    public AudioClip shootSound;           // Sound to play when shooting
    public GameObject muzzleFlashPrefab; // Prefab of the muzzle flash effect

    private AudioSource audioSource;       // Reference to the AudioSource component
    private bool isShooting;               // Flag to track if the player is shooting
    private bool canShoot = true;          // Flag to control shooting rate

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for shooting input
        if (isShooting && canShoot)
        {
            StartCoroutine(ContinuousFire());
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
            // Reset canShoot flag
            canShoot = true;
        }
    }

    IEnumerator ContinuousFire()
    {
        canShoot = false; // Prevent shooting during the firing cooldown
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(1f / fireRate);
        }
        canShoot = true; // Allow shooting after the firing cooldown
    }

    void Shoot()
    {
        // Play the shooting sound
        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);

        // Instantiate a new projectile for each barrel
        foreach (Transform barrelTransform in barrelTransforms)
        {
            GameObject projectile = Instantiate(projectilePrefab, barrelTransform.position, barrelTransform.rotation);
            Transform look = new GameObject().transform;
            look.position = projectile.transform.position;
            look.LookAt(PlayerMechController.me.lookpos.position);
            projectile.transform.eulerAngles = new Vector3(look.eulerAngles.x, projectile.transform.eulerAngles.y, projectile.transform.eulerAngles.z);
            Destroy(look.gameObject);

            // Instantiate muzzle flash effect at the fire point
            if (muzzleFlashPrefab)
            {
                GameObject newMuzzle = Instantiate(muzzleFlashPrefab, barrelTransform.position, barrelTransform.rotation);
                newMuzzle.transform.parent = transform;
            }
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(projectile.transform.forward * shootForce, ForceMode.Impulse);
            }
            // Destroy the projectile after a delay
            Destroy(projectile, 3f);
        }
    }
}