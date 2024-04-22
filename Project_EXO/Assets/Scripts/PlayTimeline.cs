using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayTimeline: MonoBehaviour
{
    public PlayableDirector timeline; // Reference to the Timeline that you want to play
    private bool hasPlayed = false; // Flag to track if the timeline has been played

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            if (timeline != null)
            {
                timeline.Play(); // Play the Timeline
                hasPlayed = true; // Set the flag to true to indicate that the timeline has been played
            }
            else
            {
                Debug.LogWarning("No Timeline assigned to PlayTimelineOnceOnTriggerEnter script!");
            }
        }
    }
}