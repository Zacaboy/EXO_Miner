using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    public Transform ground;

    public void OnTriggerEnter(Collider other)
    {
        ground = other.transform;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform == ground)
            ground = null;
    }
}
