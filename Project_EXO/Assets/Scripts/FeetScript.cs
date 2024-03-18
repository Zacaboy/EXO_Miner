using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    public List<Transform> ground = new List<Transform>();

    public void OnTriggerEnter(Collider other)
    {
        if (!ground.Contains(other.transform))
            ground.Add(other.transform);
    }
    public void OnTriggerExit(Collider other)
    {
        if (ground.Contains(other.transform))
            ground.Remove(other.transform);
    }
}
