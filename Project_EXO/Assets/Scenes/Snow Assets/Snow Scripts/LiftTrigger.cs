using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public LiftScript liftScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (liftScript.liftBottom == false)
        {
            liftScript.LiftDown();
            Debug.Log("Lift Triggered!");
        }

        if (liftScript.liftBottom == true)
        {
            liftScript.LiftUp();
            Debug.Log("Lift Triggered!");
        }
    }
}
