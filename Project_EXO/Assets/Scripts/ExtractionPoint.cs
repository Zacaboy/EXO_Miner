using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    [HideInInspector] public Objective objective;

    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeSinceLevelLoad <= 2)
            transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (GameManager.me.over) return;
        if (other.transform == PlayerMechController.me.transform)
        {
            if (objective != null)
                ObjectiveManager.me.CompleteObjective(objective.name);
            GameManager.me.CompleteObjective();
        }
    }
}
