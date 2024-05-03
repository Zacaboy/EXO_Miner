using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObjectiveTracker : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    [HideInInspector] public Transform targetLocation;

    private void Start()
    {
        if (distanceText == null)
        {
            Debug.LogError("Distance text object is not assigned!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Check if any objects are being tracked
        if (targetLocation)
        {
            // Calculate the distance between player and target location in meters
            float distance = Vector3.Distance(transform.position, targetLocation.position);

            // Update the text to display the distance in meters
            distanceText.text = "Distance: " + distance.ToString("F2") + " meters";
        }
        else
        {
            distanceText.text = "Explore The Environment";
        }

        // This finds the current objective
        if (ObjectiveManager.me.currentObjective.type != ObjectiveType.CollectResources)
        {
            if (ObjectiveManager.me.currentObjective.group)
                targetLocation = ObjectiveManager.me.currentObjective.group.transform;
            else
                targetLocation = null;
        }
        else
            targetLocation = null;

    }
}