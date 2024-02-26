using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining_Lazer : MonoBehaviour
{
    public bool isFiring;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartFire();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            StopFire();

        if (isFiring)
        {

        }
        else
        {

        }
    }

    public void StartFire()
    {
        isFiring = true;

    }

    public void StopFire()
    {
        isFiring = false;
    }
}
