using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public static MapUI me;
    public CanvasGroup mapGroup;
    public Image mapPanel;
    float spawnTime;

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mapGroup.alpha = 0;
        mapPanel.GetComponent<CanvasGroup>().alpha = 0;
        spawnTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.me.mapIcon)
        {
            if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 0.8f)
                mapGroup.alpha = Mathf.Lerp(mapGroup.alpha, 1, 0.002f);
            if (Timer.me & Time.timeSinceLevelLoad >= spawnTime + 1.6f)
            {
                mapPanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(mapPanel.GetComponent<CanvasGroup>().alpha, 1, 0.009f);
                mapPanel.sprite = GameManager.me.mapIcon;
            }
        }
    }
}
