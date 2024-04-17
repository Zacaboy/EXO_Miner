using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OreType
{
    public string name;
    [HideInInspector] public int amount;
}

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker me;
    [SerializeField] public List<OreType> oreTypes = new List<OreType>();

    private void Awake()
    {
        me = this;
    }

    public void AddResource(string name, int amount)
    {
        OreType hasOre = null;
        foreach (OreType type in oreTypes)
            if (type.name == name)
                hasOre = type;
        if (hasOre != null)
            hasOre.amount += amount;
        else
        {
            hasOre = new OreType { name = name, amount = amount };
            oreTypes.Add(hasOre);
        }
        if (ResourceUI.me)
            ResourceUI.me.AddResource(name, amount, hasOre.amount);
        if (ObjectiveManager.me)
            ObjectiveManager.me.UpdateType(ObjectiveType.CollectResources, amount);
    }
}
