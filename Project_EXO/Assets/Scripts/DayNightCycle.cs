using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayLengthInMinutes = 1f; // Duration of one day in minutes
    public Transform sunTransform; // Reference to the sun/moon object to rotate
    public float startAngle = 0f; // Starting rotation angle of the sun

    private const float totalAngle = 360f; // Total angle for a full rotation (360 degrees)
    private float secondsPerDegree; // Seconds per degree of rotation

    void Start()
    {
        secondsPerDegree = (dayLengthInMinutes * 60f) / totalAngle; // Calculate seconds per degree
        RotateSun();
    }

    void Update()
    {
        RotateSun();
    }

    void RotateSun()
    {
        float currentAngle = (startAngle + (Time.time / secondsPerDegree) % totalAngle) % totalAngle;
        sunTransform.rotation = Quaternion.Euler(currentAngle, 0, 0);
    }
}