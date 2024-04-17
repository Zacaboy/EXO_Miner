using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
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
}
