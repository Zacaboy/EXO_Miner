// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Define the Flashlight class
public class Flashlight : MonoBehaviour
{
    // Declare public variables for flashlight components and audio
    public Light flashlightFront; // The front light of the flashlight
    public Light flashlightBack; // The back light of the flashlight
    public AudioSource audioSource; // The audio source for playing sound effects
    public AudioClip toggleSound; // The sound clip for toggling the flashlight

    // Start is called before the first frame update
    void Start()
    {
        // Initialize both flashlight lights as disabled
        flashlightFront.enabled = false;
        flashlightBack.enabled = false;
    }

    // Method to toggle the flashlight
    public void ToggleFlashlight(InputAction.CallbackContext context)
    {
        // Check if the input action has started
        if (context.started)
        {
            // Toggle the enabled state of both flashlight lights
            flashlightFront.enabled = !flashlightFront.enabled;
            flashlightBack.enabled = !flashlightBack.enabled;

            // If there is a toggle sound clip and an audio source attached, play the sound
            if (toggleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }
    }
}