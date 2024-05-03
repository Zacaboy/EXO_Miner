using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed at which the lift moves
    public int lowestPoint = 0; // Lowest point the lift can reach
    public int highestPoint = 600; // Highest point the lift can reach
    public bool liftBottom; // Indicates if the lift is at the bottom position
    public bool liftActive; // Indicates if the lift is actively moving
    public bool liftPending;

    // Start is called before the first frame update
    void Start()
    {
        liftBottom = false; // Initialize liftBottom to false
        liftActive = false; // Initialize liftActive to false
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the lift is at the lowest point
        if (transform.position.y <= lowestPoint && liftPending == false)
        {
            liftBottom = true; // Set liftBottom to true
            liftActive = false; // Set liftActive to false
            liftPending = true;
        }

        // Check if the lift is at the highest point
        if (transform.position.y >= highestPoint && liftPending == false)
        {
            liftBottom = false; // Set liftBottom to false
            liftActive = false; // Set liftActive to false
            liftPending = true;
        }

        if (transform.position.y <= 330 && transform.position.y >= 270)
        {
            liftPending = false;
        }
    }

    // Method to move the lift down
    public void LiftDown()
    {
        // Check if the lift can move down without going below the lowest point
        if (transform.position.y >= lowestPoint)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); // Move the lift down
            Debug.Log("Lift Moving Down!"); // Log a message indicating the lift is moving down
        }
    }

    // Method to move the lift up
    public void LiftUp()
    {
        // Check if the lift can move up without going above the highest point
        if (transform.position.y <= highestPoint)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // Move the lift up
            Debug.Log("Lift Moving Up!"); // Log a message indicating the lift is moving up
        }
    }
}
