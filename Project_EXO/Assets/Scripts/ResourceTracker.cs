using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        me = this;
        foreach (OreType type in oreTypes)
            if (type.essential)
                neededOres.Add(type);
            else
                extraOres.Add(type);
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
            GameManager.me.CompleteObjective();
    }
}
