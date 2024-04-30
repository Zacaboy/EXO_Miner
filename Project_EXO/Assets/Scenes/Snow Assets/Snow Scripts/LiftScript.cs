using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float lowestPoint = 0;
    public float highestPoint = 600;
    public bool liftBottom;
    public bool liftActive;

    // Start is called before the first frame update
    void Start()
    {
        liftBottom = false;
        liftActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= lowestPoint)
        {
            liftBottom = true;
            liftActive = false;
        }

        if (transform.position.y >= highestPoint)
        {
            liftBottom = false;
            liftActive = false;
        }
    }

    public void LiftDown()
    {
        if (transform.position.y >= lowestPoint)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            Debug.Log("Lift Moving Down!");
        }
    }

    public void LiftUp()
    {
        if (transform.position.y <= highestPoint)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            Debug.Log("Lift Moving Up!");
        }
    }
}
