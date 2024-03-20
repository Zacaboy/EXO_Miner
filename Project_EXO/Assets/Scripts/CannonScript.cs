using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public Transform firePoint; // Point from which the projectile will be fired
    public float shootForce = 10f; // Force at which the projectile is shot
    public float destroyDelay = 3f; // Time after which the projectile will be destroyed
    public AudioClip shootSound; // Sound to play when shooting
    public GameObject muzzleFlashPrefab; // Prefab of the muzzle flash effect

    private AudioSource audioSource; // Reference to the AudioSource component

    private bool isShooting; // Flag to track if the player is shooting

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for shooting input
        if (isShooting)
        {
            Shoot();
            // Reset the flag to prevent continuous shooting
            isShooting = false;
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
    }

    void Shoot()
    {
        // Play the shooting sound
        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);
        GetComponentInParent<PlayerMechController>().FireWeapon();

        // Instantiate muzzle flash effect at the fire point
        if (muzzleFlashPrefab)
        {
            GameObject newMuzzle = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            newMuzzle.transform.parent = transform;
        }

        // Instantiate a new projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Transform look = new GameObject().transform;
        look.position = projectile.transform.position;
        look.LookAt(PlayerMechController.me.lookpos.position);
        projectile.transform.eulerAngles = new Vector3(look.eulerAngles.x, projectile.transform.eulerAngles.y, projectile.transform.eulerAngles.z);
        projectile.GetComponent<ProjectileScript>().shooter = gameObject;
        Destroy(look.gameObject);

        // Add force to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(projectile.transform.forward * shootForce, ForceMode.Impulse);
        }

        // Destroy the projectile after a delay
        Destroy(projectile, destroyDelay);
    }
}