using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public GameObject player;
    public GameObject lift;
    public float moveSpeed = 5f;
    public float lowestPoint = 0;
    public bool liftBottom;

    // Start is called before the first frame update
    void Start()
    {
        liftBottom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0)
        {
            liftBottom = true;
        }

        if (transform.position.y <= 600)
        {
            liftBottom = false;
        }
    }

    public void LiftDown()
    {
        if (transform.position.y >= 0 && liftBottom == false)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            Debug.Log("Lift Moving Down!");
            SetParent();
        }
    }

    public void LiftUp()
    {
        if (transform.position.y <= 600 && liftBottom == true)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            Debug.Log("Lift Moving Up!");
        }
    }

    public void SetParent(GameObject newParent)
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        player.transform.parent = newParent.transform;

        //Display the parent's name in the console.
        Debug.Log("Player's Parent: " + player.transform.parent.name);

        // Check if the new parent has a parent GameObject.
        if (newParent.transform.parent != null)
        {
            //Display the name of the grand parent of the player.
            Debug.Log("Player's Grand parent: " + player.transform.parent.parent.name);
        }
    }

    public void DetachFromParent()
    {
        // Detaches the transform from its parent.
        transform.parent = null;
    }
}
