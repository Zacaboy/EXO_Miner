using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ObjectiveType { CollectResources, EliminateEnemies, DestroyTarget, MoveToExtraction }

[Serializable]
public class Objective
{
    public string name;
    public ObjectiveType type;

    [Header("Progress")]
    public int requiredProgress = 15;
    public bool showProgress = true;

    [Header("Group And Targets")]
    public GameObject group;
    public bool destroyTargets;
    public bool failIfNoTargetsAvailable = true;
    public bool hardFail; // This fails the whole game

    [Header("Ignore")]
    [HideInInspector] public List<Transform> targets = new List<Transform>();
    [SerializeField] public List<Objective> nextObjective = new List<Objective>();
    [HideInInspector] public int progress;
    [HideInInspector] public bool completed;
    [HideInInspector] public bool failed;
}


public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager me;
    [SerializeField] public List<Objective> objectives = new List<Objective>();
    public Transform extractionPointPref;
    [HideInInspector] public Objective currentObjective;

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Objective objective in objectives)
        {
            if (objective.group)
                objective.group.SetActive(false);
            foreach (Objective next in objective.nextObjective)
                if (next.group)
                    next.group.SetActive(false);
        }
        if (objectives.Count > 0)
            AddObjective(objectives[0]);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentObjective != null)
            if (currentObjective.failIfNoTargetsAvailable)
            {
                foreach(Transform target in currentObjective.targets)
                    if(!target)
                        currentObjective.targets.Remove(target);
                if (currentObjective.targets.Count == 0 & currentObjective.progress < currentObjective.requiredProgress)
                    FailObjective(currentObjective.name);
            }
    }

    public void AddObjective(Objective objective)
    {
        if (!objectives.Contains(objective))
        {
            objectives.Add(objective);
            if (ObjectiveUI.me)
                ObjectiveUI.me.AddObjective(objective);
        }
        if (objective.group)
        {
            foreach (Health health in objective.group.GetComponentsInChildren<Health>())
                if (!objective.targets.Contains(health.transform))
                    objective.targets.Add(health.transform);
            if (objective.requiredProgress == 0)
                objective.requiredProgress = objective.targets.Count;
            objective.group.SetActive(true);
        }
        else if (objective.type == ObjectiveType.CollectResources)
            foreach (Ore ore in FindObjectsOfType<Ore>())
                if (!objective.targets.Contains(ore.transform))
                    objective.targets.Add(ore.transform);
        foreach (Transform target in objective.targets)
            if (target.GetComponent<Health>())
            {
                target.GetComponent<Health>().objectiveName = objective.name;
                target.GetComponent<Health>().objective = objective;
            }
        if (objective.type == ObjectiveType.MoveToExtraction)
        {
            if (!objective.group)
            {
                Transform extraction = Instantiate(extractionPointPref);
                extraction.transform.position = GameManager.me.startPos;
                extraction.GetComponentInChildren<ExtractionPoint>().objective = objective;
            }
            else if (objective.group.transform.GetComponentInChildren<ExtractionPoint>())
                objective.group.transform.GetComponentInChildren<ExtractionPoint>().objective = objective;
        }
        SwitchObjective(objective.name);
    }
    public void SwitchObjective(string objective)
    {
        foreach (Objective type in objectives)
            if (type.name == objective)
            {
                currentObjective = type;
                if (ObjectiveUI.me)
                    ObjectiveUI.me.AddObjective(type);
            }
    }

    public void UpdateProgress(string objective, int progress)
    {
        foreach (Objective type in objectives)
            if (type.name == objective)
            {
                type.progress += progress;
                if(type.progress >= type.requiredProgress)
                    CompleteObjective(objective);
            }
    }
    public void UpdateType(ObjectiveType type, int amount)
    {
        foreach (Objective objective in objectives)
            if (objective.type == ObjectiveType.CollectResources)
                UpdateProgress(objective.name, amount);
    }

    public Objective FindObjective(string name)
    {
        Objective objective = null;
        foreach (Objective type in objectives)
            if (type.name == name)
                objective = type;
        return objective;
    }

    public void CompleteObjective(string objective)
    {
        foreach (Objective type in objectives)
            if (type.name == objective)
            {
                type.completed = true;
                currentObjective = null;
                if (type.nextObjective.Count > 0)
                    AddObjective(type.nextObjective[0]);
            }
    }
    public void FailObjective(string objective)
    {
        foreach (Objective type in objectives)
            if (type.name == objective)
            {
                type.failed = true;
                if (type.hardFail)
                    GameManager.me.FailObjective(false);
                else
                    AddObjective(type.nextObjective[0]);
            }
    }
}
