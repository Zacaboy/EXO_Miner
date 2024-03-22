using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationManager : MonoBehaviour
{
    public static OptimizationManager me;
    public float distance = 400;
    [HideInInspector] public List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (BugScript obj in FindObjectsOfType<BugScript>())
            objects.Add(obj.gameObject);
        foreach (Ore obj in FindObjectsOfType<Ore>())
            objects.Add(obj.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in objects)
            if (obj)
                obj.SetActive(Vector3.Distance(Camera.main.transform.position, obj.transform.position) <= distance);
    }
}
