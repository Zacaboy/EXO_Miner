using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Adds this object to the OptimizationManager for better performance
        if (OptimizationManager.me)
            OptimizationManager.me.objects.Add(gameObject);
    }
}
