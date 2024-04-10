using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTracker : MonoBehaviour
{
    public Transform initialTargetLocation;
    public Transform alternateTargetLocation;
    public TMP_Text distanceText;
    public GameObject[] objectsToTrack;

    public Transform targetLocation;

    private void Start()
    {
        if (initialTargetLocation == null)
        {
            Debug.LogError("Initial target location is not assigned!");
            enabled = false;
        }
        else
        {
            targetLocation = initialTargetLocation;
        }

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
            distanceText.text = "Distance To Target: " + distance.ToString("F2") + "";
        }
        if (objectsToTrack.Length == 0)
        {
            // Change the target location to the alternate one
            if (alternateTargetLocation != null)
            {
                targetLocation = alternateTargetLocation;
            }
            else
            {
                Debug.LogError("Alternate target location is not assigned!");
                enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if any tracked objects have been destroyed
        for (int i = 0; i < objectsToTrack.Length; i++)
        {
            if (objectsToTrack[i] == null)
            {
                // Remove the destroyed object from the array
                objectsToTrack[i] = objectsToTrack[objectsToTrack.Length - 1];
                System.Array.Resize(ref objectsToTrack, objectsToTrack.Length - 1);
            }
        }
    }
}