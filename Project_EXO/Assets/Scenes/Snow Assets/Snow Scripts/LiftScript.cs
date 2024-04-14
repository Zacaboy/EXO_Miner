using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lowestPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LiftDown()
    {
        if (transform.position.y >= 0)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            Debug.Log("Lift Moving Down!");
        }
    }
}
