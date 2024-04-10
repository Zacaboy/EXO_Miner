using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    public Transform player; // Player's transform
    [HideInInspector] public Ore[] objectsToTrack; // Array of game objects to track
    public SpriteRenderer spriteRenderer; // SpriteRenderer for displaying images
    public Sprite noObjectsInRangeImage; // Image for no objects within range
    public TMP_Text distanceText; // TextMeshPro component to display distance information
    public Light m_Light;
    public float minimumRange = 5f; // Minimum range to detect objects

    [Header("UI")]
    public SpriteRenderer panel;
    public Color outofRangeCol = Color.red;

    private Ore closestObject; // Closest object to the player
    Color startCol;
    float startTime;
    Color normalCol;
    float[] startAlphas;
    float startLight;

    void Start()
    {
        objectsToTrack = FindObjectsOfType<Ore>();
        startCol = distanceText.color;
        startTime = Time.timeSinceLevelLoad;
        startAlphas = new float[3];
        startAlphas[0] = spriteRenderer.color.a;
        startAlphas[1] = panel.color.a;
        startAlphas[2] = distanceText.color.a;
        normalCol = panel.color;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        distanceText.color = new Color(distanceText.color.r, distanceText.color.g, distanceText.color.b, 0);
        distanceText.text = "Starting...";
        distanceText.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0);
        startLight = m_Light.intensity;
        m_Light.intensity = 0;
    }

    void Update()
    {
        objectsToTrack = FindObjectsOfType<Ore>();
        if (Time.timeSinceLevelLoad >= startTime + 2.5f)
        {
            bool show = true;
            if (Mining_UI.me)
                if (Mining_UI.me.lazer.currentOre)
                    show = false;
            if (show)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, startAlphas[0], 0.01f));
            else
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, 0, 0.01f));
        }
        if (Time.timeSinceLevelLoad >= startTime + 1)
        {
            m_Light.intensity = Mathf.Lerp(m_Light.intensity, startLight, 0.005f);
            if (closestObject)
                panel.color = Color.Lerp(panel.color, normalCol, 0.05f);
            else
                panel.color = Color.Lerp(panel.color, outofRangeCol, 0.05f);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Lerp(panel.color.a, startAlphas[1], 0.001f));
            distanceText.color = new Color(distanceText.color.r, distanceText.color.g, distanceText.color.b, Mathf.Lerp(distanceText.color.a, startAlphas[2], 0.01f));

            float minDistance = Mathf.Infinity;
            closestObject = null;

            // Remove null objects from the objectsToTrack array
            objectsToTrack = RemoveDestroyedObjects(objectsToTrack);

            foreach (Ore obj in objectsToTrack)
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

            if (closestObject)
            {
                // Display the sprite for closest object
                spriteRenderer.sprite = closestObject.icon;
                if (Time.timeSinceLevelLoad >= startTime + 2.5f)
                {
                    distanceText.text = "Distance: " + Mathf.Round(minDistance) + "m";
                    distanceText.color = startCol;
                }
            }
            else
            {
                // Display the sprite for no objects within range
                spriteRenderer.sprite = noObjectsInRangeImage;
                if (Time.timeSinceLevelLoad >= startTime + 2.5f)
                {
                    distanceText.text = "Out of Range";
                    distanceText.color = Color.red;
                }
            }
        }        
    }

    // Method to remove destroyed/null objects from the objectsToTrack array
    Ore[] RemoveDestroyedObjects(Ore[] objects)
    {
        // Filter out null or destroyed objects
        return System.Array.FindAll(objects, obj => obj != null);
    }
}