using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCallUp : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (liftScript.liftBottom == true)
        {
            liftScript.liftActive = true;
        }
    }
}
