using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (OptimizationManager.me)
            OptimizationManager.me.objects.Add(gameObject);
    }
}
