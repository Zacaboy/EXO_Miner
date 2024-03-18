using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Mining_UI : MonoBehaviour
{
    public static Mining_UI me;
    public CanvasGroup ui;
    public Image health;
    public Image healthAfter;

    [HideInInspector] public Mining_Lazer lazer;
    [HideInInspector] public float healthAmount;
    Animator ani;
    float lastMoveTime;
    float lastFlashTime;
    Ore previousOre;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        ani = GetComponent<Animator>();
        lazer = FindObjectOfType<Mining_Lazer>();
        ui.alpha = 0;
    }

    void FixedUpdate()
    {
        if (lazer.currentOre != previousOre)
            if (lazer.currentOre)
            {
                health.sprite = lazer.currentOre.icon;
                healthAfter.sprite = lazer.currentOre.icon;
                healthAmount = lazer.currentOre.health / lazer.currentOre.maxHealth;
                health.fillAmount = healthAmount;
                healthAfter.fillAmount = healthAmount;
            }
        if (lazer.currentOre || healthAfter.fillAmount - health.fillAmount >= 0.01f || lastMoveTime > 0 & Time.fixedTime >= lastMoveTime + 0.8f)
        {
            ui.alpha = Mathf.Lerp(ui.alpha, 1, 0.2f);
            lastMoveTime = Time.fixedTime;
            if (lazer.currentOre)
                healthAmount = lazer.currentOre.health / lazer.currentOre.maxHealth;
            health.fillAmount = Mathf.Lerp(health.fillAmount, healthAmount, 0.1f);
            if (Time.fixedTime >= lazer.lastMineTime + 0.1f)
                if (lazer.isFiring & lazer.currentOre)
                    healthAfter.fillAmount = Mathf.Lerp(healthAfter.fillAmount, health.fillAmount, 0.2f);
                else
                    healthAfter.fillAmount = Mathf.Lerp(healthAfter.fillAmount, health.fillAmount, 0.08f);
        }
        else
        {
            lastMoveTime = 0;
            ui.alpha = Mathf.Lerp(ui.alpha, 0, 0.09f);
            if (ui.alpha <= 0.01f)
            {
                health.fillAmount = 1;
                healthAfter.fillAmount = 1;
                healthAmount = 0;
            }
        }
        previousOre = lazer.currentOre;
    }

    public void Flash()
    {
        if (Time.fixedTime < lastFlashTime + 0.35f) return;
        lastFlashTime = Time.fixedTime;
        ani.CrossFadeInFixedTime("Flash", 0.1f);
    }
}
