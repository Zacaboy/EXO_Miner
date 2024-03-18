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
    public TextMeshProUGUI timerT;
    public Image timerPanel;
    public CanvasGroup timePanel;
    public CanvasGroup timerBackgroundPanel;
    public CanvasGroup resourcePanel;
    public CanvasGroup essentialPanel;
    public CanvasGroup extraPanel;
    public TextMeshProUGUI essentialT;
    public TextMeshProUGUI extraT;
    public TextMeshProUGUI essentialOrePref;
    public TextMeshProUGUI extraOrePref;
    public UIMessageScript winUI;
    public UIMessageScript failUI;
    CanvasGroup canvas;
    [HideInInspector] public List<TextMeshProUGUI> needed = new List<TextMeshProUGUI>();
    [HideInInspector] public List<TextMeshProUGUI> extra = new List<TextMeshProUGUI>();
    [SerializeField] List<ResourcesFeedback> feedback = new List<ResourcesFeedback>();
    float spawnTime;
    float timerFloat;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        timePanel.alpha = 0;
        timerBackgroundPanel.alpha = 0;

        resourcePanel.alpha = 0;

        essentialPanel.alpha = 0;
        extraPanel.alpha = 0;

        timerBackgroundPanel.transform.SetParent(transform);
        timePanel.transform.SetParent(transform);

        essentialPanel.transform.SetParent(transform);
        extraPanel.transform.SetParent(transform);

        essentialOrePref.gameObject.SetActive(false);
        extraOrePref.gameObject.SetActive(false);

        spawnTime = Time.timeSinceLevelLoad;
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        timerT.GetComponent<CanvasGroup>().alpha = 0;
        if (FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().completeEvent.AddListener(Win);
            FindObjectOfType<GameManager>().failEvent.AddListener(Fail);
        }
        timerT.text = "";
        timerPanel.fillAmount = 0;
        timerBackgroundPanel.GetComponent<Image>().fillAmount = 0;
        if (FindObjectOfType<ResourceTracker>())
        {
            foreach (OreType ore in FindObjectOfType<ResourceTracker>().neededOres)
            {
                TextMeshProUGUI text = Instantiate(essentialOrePref, essentialOrePref.transform.parent);
                text.gameObject.SetActive(true);
                text.name = ore.name;
                needed.Add(text);
            }
            foreach (OreType ore in FindObjectOfType<ResourceTracker>().extraOres)
            {
                TextMeshProUGUI text = Instantiate(extraOrePref, extraOrePref.transform.parent);
                text.gameObject.SetActive(true);
                text.name = ore.name;
                extra.Add(text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 0.8f)
        {
            timePanel.alpha = Mathf.Lerp(timePanel.alpha, 1, 0.002f);
            timerBackgroundPanel.alpha = Mathf.Lerp(timerBackgroundPanel.alpha, 1, 0.005f);
            timerBackgroundPanel.GetComponent<Image>().fillAmount = Mathf.Lerp(timerBackgroundPanel.GetComponent<Image>().fillAmount, 1, 0.05f);
        }
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 2)
        {
            timerT.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(timerT.GetComponent<CanvasGroup>().alpha, 1, 0.005f);
            timerFloat = Mathf.Lerp(timerFloat, Timer.me.time, 0.05f);
            timerT.text = Mathf.Round(timerFloat).ToString();
            if (Timer.me.noLimit)
                timerPanel.fillAmount = Timer.me.time / Timer.me.maxTime;
            else
                timerPanel.fillAmount = Mathf.Lerp(timerPanel.fillAmount, Timer.me.time / Timer.me.maxTime, 0.05f);
            if (Timer.me.time <= 2)
                timerT.color = Color.red;
        }
        if (ResourceTracker.me & Time.timeSinceLevelLoad >= spawnTime + 1.2f)
            essentialPanel.alpha = Mathf.Lerp(essentialPanel.alpha, 1, 0.005f);
        if (ResourceTracker.me & Time.timeSinceLevelLoad >= spawnTime + 1.8f)
            extraPanel.alpha = Mathf.Lerp(extraPanel.alpha, 1, 0.005f);
        if (ResourceTracker.me & Time.timeSinceLevelLoad >= spawnTime + 2.1f)
        {
            if (ResourceTracker.me.oreTypes.Count > 0)
            {
                resourcePanel.alpha = Mathf.Lerp(resourcePanel.alpha, 1, 0.005f);
                for (int i = 0; i < needed.Count; i++)
                {
                    needed[i].text = "- " + ResourceTracker.me.neededOres[i].name + " " + ResourceTracker.me.neededOres[i].currentAmount + "/" + ResourceTracker.me.neededOres[i].amountRequired;
                    if (ResourceTracker.me.neededOres[i].currentAmount >= ResourceTracker.me.neededOres[i].amountRequired)
                        needed[i].color = Color.green;
                }
                for (int i = 0; i < extra.Count; i++)
                {
                    extra[i].text = "- " + ResourceTracker.me.extraOres[i].name + " " + ResourceTracker.me.extraOres[i].currentAmount + "/" + ResourceTracker.me.extraOres[i].amountRequired;
                    if (ResourceTracker.me.extraOres[i].currentAmount >= ResourceTracker.me.extraOres[i].amountRequired)
                        extra[i].color = Color.green;
                }
            }
            else
                resourcePanel.alpha = Mathf.Lerp(resourcePanel.alpha, 0, 0.1f);
        }
        canvas.alpha = Mathf.Lerp(resourcePanel.alpha, 1, 0.2f);
        foreach (ResourcesFeedback feed in feedback)
        {
            if (Time.time >= feed.startTime + 0.06f)
                feed.currentAmount = Mathf.Lerp(feed.currentAmount, feed.amount, 0.01f);
            feed.text.text = "+ " + Mathf.Round(feed.currentAmount).ToString();
            if (feed.currentAmount >= feed.amount)
                feedback.Remove(feed);
        }
    }

    public void AddResource(string name, int amount)
    {
        foreach (TextMeshProUGUI text in needed)
            if (text.name == name)
            {
                text.GetComponent<Animator>().CrossFadeInFixedTime("Flash", 0.1f);
                feedback.Add(new ResourcesFeedback() { text = text.transform.GetChild(0).GetComponent<TextMeshProUGUI>(), amount = amount, startTime = Time.time });
            }
        foreach (TextMeshProUGUI text in extra)
            if (text.name == name)
            {
                text.GetComponent<Animator>().CrossFadeInFixedTime("Flash", 0.1f);
                feedback.Add(new ResourcesFeedback() { text = text.transform.GetChild(0).GetComponent<TextMeshProUGUI>(), amount = amount, startTime = Time.time });
            }
    }

    public void Win()
    {
        UIMessageScript ui = Instantiate(winUI);
    }
    public void Fail()
    {
        UIMessageScript ui = Instantiate(failUI);
    }
}
