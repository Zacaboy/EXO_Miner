using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public GameObject sun;
    public float cycleDurationInMinutes = 120f; // Duration of each cycle in minutes (2 hours)
    private float cycleProgress = 0f; // Current progress of the cycle in seconds

    void Start()
    {
        // Load saved cycle progress if available
        cycleProgress = PlayerPrefs.GetFloat("CycleProgress", 0f);
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

        // Update sun position
        UpdateSunPosition();
    }

    void UpdateSunPosition()
    {
        // Calculate the angle of the sun based on the cycle progress
        float angle = Mathf.Lerp(-90f, 270f, cycleProgress / (cycleDurationInMinutes * 60f));

        // Update sun rotation
        sun.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
    }

    void OnApplicationQuit()
    {
        // Save cycle progress when the application quits
        PlayerPrefs.SetFloat("CycleProgress", cycleProgress);
        PlayerPrefs.Save();
    }
}