using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jungle_ShroomScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("Collision Player!");
            collision.rigidbody.AddForce(transform.up*800f,ForceMode.Impulse);
        }

    }
    
}
