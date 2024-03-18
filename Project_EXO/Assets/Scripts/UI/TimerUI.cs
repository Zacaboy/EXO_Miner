using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public static TimerUI me;
    public TextMeshProUGUI timerT;
    public TextMeshProUGUI warningT;
    public CanvasGroup timePanel;
    public Image timerPanel;
    public CanvasGroup timerBackgroundPanel;
    public bool showTime;
    float timerFloat;
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        me = this;

        timePanel.alpha = 0;
        timerBackgroundPanel.alpha = 0;

        if (Timer.me)
            enabled = showTime & Timer.me.showTime;
        else
            enabled = false;
        if (showTime)
        {
            timerBackgroundPanel.transform.SetParent(transform);
            timePanel.transform.SetParent(transform);
        }

        timerT.GetComponent<CanvasGroup>().alpha = 0;
        timerT.text = "";
        warningT.GetComponent<CanvasGroup>().alpha = 0;
        timerPanel.fillAmount = 0;
        timerBackgroundPanel.GetComponent<Image>().fillAmount = 0;
        spawnTime = Time.timeSinceLevelLoad;
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
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 1.6f)
            warningT.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(warningT.GetComponent<CanvasGroup>().alpha, 1, 0.005f);
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 2)
        {
            timerT.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(timerT.GetComponent<CanvasGroup>().alpha, 1, 0.005f);
            timerFloat = Mathf.Lerp(timerFloat, Timer.me.time, 0.05f);
            timerT.text = Mathf.Round(timerFloat).ToString();
            if (Timer.me.noLimit)
                timerPanel.fillAmount = 1;
            else
            {
                timerPanel.fillAmount = Mathf.Lerp(timerPanel.fillAmount, Timer.me.time / Timer.me.maxTime, 0.05f);
                if (Timer.me.time <= 2)
                    timerT.color = Color.red;
            }
        }
    }
}
