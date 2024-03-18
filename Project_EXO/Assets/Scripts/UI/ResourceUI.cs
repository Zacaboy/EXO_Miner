using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class ResourcesFeedback
{
    public TextMeshProUGUI text;
    public int amount;
    public float currentAmount;
    public float startTime;
}

public class ResourceUI : MonoBehaviour
{
    public static ResourceUI me;
    public CanvasGroup resourceParent;
    public CanvasGroup resourcePanel;
    public TextMeshProUGUI resourcePref;
    CanvasGroup canvas;
    [HideInInspector] public List<TextMeshProUGUI> resources = new List<TextMeshProUGUI>();
    [SerializeField] List<ResourcesFeedback> feedback = new List<ResourcesFeedback>();
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        resourceParent.alpha = 0;
        resourcePanel.alpha = 0;

        resourcePanel.transform.SetParent(transform);
        resourcePref.gameObject.SetActive(false);

        spawnTime = Time.timeSinceLevelLoad;
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ResourceTracker.me & Time.timeSinceLevelLoad >= spawnTime + 1.2f)
            resourcePanel.alpha = Mathf.Lerp(resourcePanel.alpha, 1, 0.005f);
        if (ResourceTracker.me & Time.timeSinceLevelLoad >= spawnTime + 1.8f)
            resourceParent.alpha = Mathf.Lerp(resourceParent.alpha, 1, 0.05f);
        canvas.alpha = Mathf.Lerp(canvas.alpha, 1, 0.2f);
        foreach (ResourcesFeedback feed in feedback)
        {
            if (Time.time >= feed.startTime + 0.06f)
                feed.currentAmount = Mathf.Lerp(feed.currentAmount, feed.amount, 0.01f);
            feed.text.text = "+ " + Mathf.Round(feed.currentAmount).ToString();
            if (feed.currentAmount >= feed.amount)
                feedback.Remove(feed);
        }
    }

    public void AddResource(string name, int amount, int maxAmount)
    {
        if (FindObjectOfType<ResourceTracker>())
        {
            TextMeshProUGUI currentText = null;
            foreach (TextMeshProUGUI text in resources)
                if (text.name == name)
                    currentText = text;
            if (currentText)
                currentText.text = "- " + name + ": " + maxAmount;
            else
            {
                TextMeshProUGUI text = Instantiate(resourcePref, resourcePref.transform.parent);
                text.gameObject.SetActive(true);
                text.name = name;
                text.text = "- " + name + ": " + maxAmount;
                text.fontSize = 0;
                resources.Add(text);
            }
        }
        foreach (TextMeshProUGUI text in resources)
            if (text.name == name)
            {
                text.GetComponent<Animator>().CrossFadeInFixedTime("Flash", 0.1f);
                feedback.Add(new ResourcesFeedback() { text = text.transform.GetChild(0).GetComponent<TextMeshProUGUI>(), amount = amount, startTime = Time.time });
            }
    }
}
