using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mining_UI : MonoBehaviour
{
    public static Mining_UI me;
    public CanvasGroup ui;
    public Image health;
    public Image healthAfter;
    public Mining_Lazer lazer;
    [HideInInspector] public float healthAmount;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        lazer = FindObjectOfType<Mining_Lazer>();
        ui.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lazer.currentOre || healthAfter.fillAmount - health.fillAmount >= 0.01f)
        {
            ui.alpha = Mathf.Lerp(ui.alpha, 1, 0.2f);
            if (lazer.currentOre)
                healthAmount = lazer.currentOre.health / lazer.currentOre.maxHealth;
            health.fillAmount = Mathf.Lerp(health.fillAmount, healthAmount, 0.1f);
            if (Time.time >= lazer.lastMineTime + 0.1f)
                if (lazer.isFiring & lazer.currentOre)
                    healthAfter.fillAmount = Mathf.Lerp(healthAfter.fillAmount, health.fillAmount, 0.2f);
                else
                    healthAfter.fillAmount = Mathf.Lerp(healthAfter.fillAmount, health.fillAmount, 0.03f);
        }
        else
        {
            ui.alpha = Mathf.Lerp(ui.alpha, 0, 0.07f);
            if (ui.alpha <= 0.1f)
            {
                health.fillAmount = 1;
                healthAfter.fillAmount = 1;
                healthAmount = 0;
            }
        }
    }
}
