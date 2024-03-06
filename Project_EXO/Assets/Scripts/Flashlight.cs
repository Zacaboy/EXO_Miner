using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public Light flashlight; // Reference to the flashlight Light component
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip toggleSound; // Sound clip to play when the flashlight is toggled

    void Start()
    {
        // Ensure the flashlight is turned off at the start
        flashlight.enabled = false;
    }

    public void ToggleFlashlight(InputAction.CallbackContext context)
    {
        // Toggle the flashlight when the input action is triggered
        if (context.started)
        {
            flashlight.enabled = !flashlight.enabled;

            // Play toggle sound if available
            if (toggleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }
    }
}