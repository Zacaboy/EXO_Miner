using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineOnDestroy : MonoBehaviour
{
    public GameObject targetObject; // Reference to the object whose destruction triggers the timeline
    public PlayableDirector timeline; // Reference to the Timeline that you want to play

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("No target object assigned to the TimelineOnDestroy script!");
        }
    }

    private void Update()
    {
        if (targetObject == null)
        {
            if (timeline != null)
            {
                timeline.Play(); // Play the Timeline
                enabled = false; // Disable this script to prevent further checks
            }
            else
            {
                Debug.LogWarning("No Timeline assigned to the TimelineOnDestroy script!");
            }
        }
    }
}