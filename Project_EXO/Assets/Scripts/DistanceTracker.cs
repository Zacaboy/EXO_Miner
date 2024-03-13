using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    public Transform player; // Player's transform
    public GameObject[] objectsToTrack; // Array of game objects to track
    public SpriteRenderer spriteRenderer; // SpriteRenderer for displaying images
    public Sprite closestObjectImage; // Image for the closest object
    public Sprite noObjectsInRangeImage; // Image for no objects within range
    public TMP_Text distanceText; // TextMeshPro component to display distance information
    public float minimumRange = 5f; // Minimum range to detect objects

    private GameObject closestObject; // Closest object to the player

    void Update()
    {
        float minDistance = Mathf.Infinity;
        closestObject = null;

        // Remove null objects from the objectsToTrack array
        objectsToTrack = RemoveDestroyedObjects(objectsToTrack);

        foreach (GameObject obj in objectsToTrack)
        {
            if (obj == null) continue; // Skip if object is null

            float distance = Vector3.Distance(player.position, obj.transform.position);

            // Check if the object is within minimum range and closer than the current closest object
            if (distance < minimumRange && distance < minDistance)
            {
                minDistance = distance;
                closestObject = obj;
            }
        }

        if (closestObject != null)
        {
            // Display the sprite for closest object
            spriteRenderer.sprite = closestObjectImage;
            distanceText.text = "Closest Object Distance: " + minDistance.ToString("F2");
        }
        else
        {
            // Display the sprite for no objects within range
            spriteRenderer.sprite = noObjectsInRangeImage;
            distanceText.text = "No objects within range";
        }
    }

    // Method to remove destroyed/null objects from the objectsToTrack array
    GameObject[] RemoveDestroyedObjects(GameObject[] objects)
    {
        // Filter out null or destroyed objects
        return System.Array.FindAll(objects, obj => obj != null);
    }
}