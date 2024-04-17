using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip to play
    private AudioSource audioSource; // The AudioSource component to play the audio clip
    private bool hasPlayed = false; // Flag to track if the audio has been played

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an audio clip is assigned to the AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource component found on the GameObject!");
        }
        else if (audioClip != null)
        {
            // Assign the audio clip to the AudioSource
            audioSource.clip = audioClip;
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to PlayAudioOnceOnTriggerEnter script!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            if (audioSource != null && audioClip != null)
            {
                // Play the audio clip from the AudioSource
                audioSource.Play();
                hasPlayed = true; // Set the flag to true to indicate that the audio has been played
            }
            else
            {
                Debug.LogWarning("AudioSource or audio clip not properly assigned!");
            }
        }
    }
}