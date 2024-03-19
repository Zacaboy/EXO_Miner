using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using TMPro;

public class EnterTriggerZone : MonoBehaviour
{
    public Behaviour[] scriptsToDeactivate;
    public PlayableDirector timeline;
    public Behaviour[] scriptsToActivate;
    public TMP_Text promptText;
    public InputActionReference inputAction;

    private bool isPlayerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            promptText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInside && inputAction.action.triggered)
        {
            StartSequence();
        }
    }

    void StartSequence()
    {
        // Deactivate the specified scripts
        foreach (var script in scriptsToDeactivate)
        {
            script.enabled = false;
        }

        // Play the specified timeline
        if (timeline != null)
        {
            timeline.Play();
        }
        else
        {
            Debug.LogError("Timeline is not assigned!");
        }
    }

    public void OnTimelineFinished()
    {
        // Activate the specified scripts
        foreach (var script in scriptsToActivate)
        {
            script.enabled = true;
        }
    }
}