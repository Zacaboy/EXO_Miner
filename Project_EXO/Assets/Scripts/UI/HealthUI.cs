using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public static HealthUI me;
    public TextMeshProUGUI healthT;
    public Image healthPanel;
    public Image healthAfterPanel;
    public CanvasGroup healthBackgroundPanel;
    public Color startCol;
    float healthFloat;
    float spawnTime;
    float lastHitTime;

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthPanel.GetComponent<CanvasGroup>().alpha = 0;
        healthBackgroundPanel.alpha = 0;
        healthAfterPanel.GetComponent<CanvasGroup>().alpha = 0;
        startCol = healthT.color;
        healthBackgroundPanel.transform.SetParent(transform);
        healthPanel.transform.SetParent(transform);
        healthAfterPanel.transform.SetParent(transform);
        healthAfterPanel.transform.SetAsFirstSibling();

        healthT.GetComponent<CanvasGroup>().alpha = 0;
        healthT.text = "";
        healthPanel.fillAmount = 0;
        healthAfterPanel.fillAmount = 0;
        healthBackgroundPanel.GetComponent<Image>().fillAmount = 0;
        spawnTime = Time.timeSinceLevelLoad;
        PlayerMechController.me.GetComponent<Health>().damageEvent.AddListener(OnHit);
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 1)
        {
            healthPanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(healthPanel.GetComponent<CanvasGroup>().alpha, 1, 0.002f);
            healthAfterPanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(healthAfterPanel.GetComponent<CanvasGroup>().alpha, 1, 0.002f);
            healthBackgroundPanel.alpha = Mathf.Lerp(healthBackgroundPanel.alpha, 1, 0.005f);
            healthBackgroundPanel.GetComponent<Image>().fillAmount = Mathf.Lerp(healthBackgroundPanel.GetComponent<Image>().fillAmount, 1, 0.015f);
        }
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 2.2)
        {
            healthPanel.fillAmount = Mathf.Lerp(healthPanel.fillAmount, (float)PlayerMechController.me.health.health / PlayerMechController.me.health.maxHealth, 0.05f);
            if (Time.time >= lastHitTime + 0.8f)
                healthAfterPanel.fillAmount = Mathf.Lerp(healthAfterPanel.fillAmount, healthPanel.fillAmount, 0.01f);
        }
        if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 2.5f)
        {
            healthT.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(healthT.GetComponent<CanvasGroup>().alpha, 1, 0.005f);
            healthFloat = Mathf.Lerp(healthFloat, PlayerMechController.me.health.health, 0.04f);
            healthT.text = Mathf.Round(healthFloat).ToString();
            if (PlayerMechController.me.health.health > 25)
                healthT.color = startCol;
            else
                healthT.color = Color.red;
        }
    }

    public void OnHit()
    {
        lastHitTime = Time.time;
    }
}

