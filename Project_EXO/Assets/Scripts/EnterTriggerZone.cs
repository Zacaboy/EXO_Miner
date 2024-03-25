using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using TMPro;

public class EnterTriggerZone : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    public PlayableDirector timeline;
    public GameObject[] objectsToActivate;
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
        // Deactivate the specified GameObjects
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
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
        StartCoroutine(WaitFinish());
    }

    public IEnumerator WaitFinish()
    {
        yield return new WaitForSeconds((float)timeline.duration);
        OnTimelineFinished();
    }

    public void OnTimelineFinished()
    {
        // Activate the specified GameObjects
        foreach (var obj in objectsToActivate)
        {
           obj.SetActive(true);
        }
    }
}