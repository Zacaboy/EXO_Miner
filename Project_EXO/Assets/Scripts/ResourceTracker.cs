using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OreType
{
    public string name;
    public int amountRequired;
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
            if (type.amountRequired == 0)
                foreach (Ore ore in FindObjectsOfType<Ore>())
                    if (ore.resourceType == type.name)
                        type.amountRequired += 1;
        foreach (OreType type in oreTypes)
            type.amountRequired = type.amountRequired /= 2;
        foreach (OreType type in oreTypes)
            if (type.essential)
                neededOres.Add(type);
            else
                extraOres.Add(type);
    }

    public void AddResource(string name, int amount)
    {
        foreach (OreType type in neededOres)
            if (type.name == name)
                type.currentAmount += amount;
        foreach (OreType type in extraOres)
            if (type.name == name)
                type.currentAmount += amount;
        if (ResourceUI.me)
            ResourceUI.me.AddResource(name, amount);
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
