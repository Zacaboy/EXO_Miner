using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject lift;
    public LiftScript liftScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (liftScript.liftBottom == false && liftScript.liftActive == true)
        {
            liftScript.LiftDown();
            Debug.Log("Lift Triggered!");
        }

        if (liftScript.liftBottom == true && liftScript.liftActive == true)
        {
            liftScript.LiftUp();
            Debug.Log("Lift Triggered!");
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        player.transform.parent = lift.transform;
        liftScript.liftActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;      
    }

}
