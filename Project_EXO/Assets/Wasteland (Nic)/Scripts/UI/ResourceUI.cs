using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class ResourceUI : MonoBehaviour
{
    public static ResourceUI me;
    public TextMeshProUGUI timerT;
    public CanvasGroup resourcePanel;
    public TextMeshProUGUI essentialT;
    public TextMeshProUGUI extraT;
    public TextMeshProUGUI essentialOrePref;
    public TextMeshProUGUI extraOrePref;
    public UIMessageScript winUI;
    public UIMessageScript failUI;
    CanvasGroup canvas;
    [HideInInspector] public List<TextMeshProUGUI> needed = new List<TextMeshProUGUI>();
    [HideInInspector] public List<TextMeshProUGUI> extra = new List<TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        resourcePanel.alpha = 0;
        essentialOrePref.gameObject.SetActive(false);
        extraOrePref.gameObject.SetActive(false);
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        FindObjectOfType<ResourceTracker>().completeEvent.AddListener(Win);
        FindObjectOfType<ResourceTracker>().failEvent.AddListener(Fail);
        foreach(OreType ore in FindObjectOfType<ResourceTracker>().neededOres)
        {
            TextMeshProUGUI text = Instantiate(essentialOrePref, essentialOrePref.transform.parent);
            text.gameObject.SetActive(true);
            needed.Add(text);
        }
        foreach (OreType ore in FindObjectOfType<ResourceTracker>().extraOres)
        {
            TextMeshProUGUI text = Instantiate(extraOrePref, extraOrePref.transform.parent);
            text.gameObject.SetActive(true);
            extra.Add(text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerT.text = Timer.me.time.ToString();
        canvas.alpha = Mathf.Lerp(resourcePanel.alpha, 1, 0.02f);
        if (Timer.me.time <= 2)
            timerT.color = Color.red;
        if (ResourceTracker.me.oreTypes.Count > 0)
        {
            resourcePanel.alpha = Mathf.Lerp(resourcePanel.alpha, 1, 0.01f);
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

    public void Win()
    {
        UIMessageScript ui = Instantiate(winUI);
    }
    public void Fail()
    {
        UIMessageScript ui = Instantiate(failUI);
    }
}
