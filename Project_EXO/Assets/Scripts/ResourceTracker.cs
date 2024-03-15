using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class OreType
{
    public string name;
    public int amountRequired = 5;
    public bool essential = true;
    [HideInInspector] public int currentAmount;
}

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker me;
    [SerializeField] public List<OreType> oreTypes = new List<OreType>();
    [HideInInspector][SerializeField] public List<OreType> neededOres = new List<OreType>();
    [HideInInspector][SerializeField] public List<OreType> extraOres = new List<OreType>();
    bool over;
    public UnityEvent completeEvent;
    public UnityEvent failEvent;

    private void Awake()
    {
        me = this;
        foreach (OreType type in oreTypes)
            if (type.essential)
                neededOres.Add(type);
            else
                extraOres.Add(type);
    }

    public void CompleteObjective()
    {
        if (over) return;
        over = true;
        completeEvent.Invoke();
        StartCoroutine(WaitFinish(2));
    }

    public void FailObjective()
    {
        if (over) return;
        over = true;
        failEvent.Invoke();
        StartCoroutine(WaitFinish(1));
    }

    public IEnumerator WaitFinish(int type)
    {
        yield return new WaitForSeconds(4);
        if (type == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (type == 2)
            SceneManager.LoadScene("Main Menu");
    }

    public void AddResource(string name)
    {
        foreach (OreType type in neededOres)
            if (type.name == name)
                type.currentAmount += 1;
        foreach (OreType type in extraOres)
            if (type.name == name)
                type.currentAmount += 1;
        CheckIfCompleted();
    }

    public void CheckIfCompleted()
    {
        int aquired = 0;
        foreach (OreType type in neededOres)
            if (type.currentAmount >= type.amountRequired)
                aquired += 1;
        if (aquired >= neededOres.Count)
            CompleteObjective();
    }
}
