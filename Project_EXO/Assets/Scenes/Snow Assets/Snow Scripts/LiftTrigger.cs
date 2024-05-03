using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject lift; // Reference to the lift GameObject
    public LiftScript liftScript; // Reference to the LiftScript attached to the lift GameObject

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the lift is active and not at the bottom, move it down
        if (liftScript.liftBottom == false && liftScript.liftActive == true)
        {
            liftScript.LiftDown(); // Call the LiftDown method from the LiftScript
            Debug.Log("Lift Triggered!"); // Log a message indicating the lift is triggered
        }

        // If the lift is active and at the bottom, move it up
        if (liftScript.liftBottom == true && liftScript.liftActive == true)
        {
            liftScript.LiftUp(); // Call the LiftUp method from the LiftScript
            Debug.Log("Lift Triggered!"); // Log a message indicating the lift is triggered
        }
    }

    // Called when the trigger collider enters another collider
    private void OnTriggerEnter(Collider other)
    {
        player.transform.parent = lift.transform; // Set the player GameObject as a child of the lift GameObject
        liftScript.liftActive = true; // Set liftActive in the LiftScript to true
    }

    // Called when the trigger collider exits another collider
    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null; // Remove the player GameObject from being a child of any object
    }

}
