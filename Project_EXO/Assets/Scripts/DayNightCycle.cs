using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float cycleDurationInMinutes = 120f; // Duration of each cycle in minutes (2 hours)
    private float cycleProgress = 0f; // Current progress of the cycle in seconds
    private float startingAngle = 0f; // Starting angle of the sun

    void Start()
    {
        // Load saved cycle progress if available
        cycleProgress = PlayerPrefs.GetFloat("CycleProgress", 0f);

        // Update the sun's position
        UpdateSunPosition();
    }

    void Update()
    {
        // Update the cycle progress
        cycleProgress += Time.deltaTime;
        if (cycleProgress >= cycleDurationInMinutes * 60f)
        {
            cycleProgress = 0f; // Reset the cycle progress
        }

        // Update the sun's position
        UpdateSunPosition();
    }

    void UpdateSunPosition()
    {
        // Calculate the angle of the sun based on the cycle progress
        float angle = Mathf.Lerp(-90f, 270f, cycleProgress / (cycleDurationInMinutes * 60f));

        // Apply the rotation to the sun, considering the starting angle
        sun.transform.rotation = Quaternion.Euler(startingAngle + angle, 0f, 0f);
    }

    void OnApplicationQuit()
    {
        // Save cycle progress when the application quits
        PlayerPrefs.SetFloat("CycleProgress", cycleProgress);
        PlayerPrefs.Save();
    }

    public void SetStartingAngle(float angle)
    {
        // Set the starting angle of the sun
        startingAngle = angle;

        // Update the sun's rotation immediately
        UpdateSunPosition();
    }
}