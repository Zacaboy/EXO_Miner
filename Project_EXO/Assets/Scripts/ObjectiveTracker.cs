using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public enum Type { Auto, Manuel }

public class ObjectiveTracker : MonoBehaviour
{
    public Type type;
    public TMP_Text distanceText;

    [Header("Manuel")]
    public Transform initialTargetLocation;
    public Transform alternateTargetLocation;
    public GameObject[] objectsToTrack;
    [HideInInspector] public Transform targetLocation;

    private void Start()
    {
        if (type == Type.Manuel)
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
            distanceText.text = "Distance: " + distance.ToString("F2") + " meters";
        }
        else
        {
            distanceText.text = "Explore The Environment";
        }

        // This is for the Auto method
        if (type == Type.Auto)
            if (ObjectiveManager.me.currentObjective.type != ObjectiveType.CollectResources)
                targetLocation = ObjectiveManager.me.currentObjective.group.transform;
            else
                targetLocation = null;

        // This is for the Manuel method
        if (objectsToTrack.Length == 0 & type == Type.Manuel)
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
        if (type == Type.Manuel)
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