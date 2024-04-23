using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class ObjectiveSlot
{
    public TextMeshProUGUI text;
    public Image progress;
    public Toggle toggle;
    public Objective Objective;
    public float progressFloat;
}

public class ObjectiveUI : MonoBehaviour
{
    public static ObjectiveUI me;
    public CanvasGroup objectivesParent;
    public CanvasGroup objectivesPanel;
    public TextMeshProUGUI objectivePref;
    public Color disabledColour = Color.white;
    public Color disabledProgressColour = Color.white;

    [Header("Aborting")]
    public CanvasGroup abortingParent;
    public Image abortingProgress;

    [SerializeField] List<ObjectiveSlot> slots = new List<ObjectiveSlot>();
    [HideInInspector] public List<TextMeshProUGUI> objectives = new List<TextMeshProUGUI>();
    Color startColour;
    Color startProgressColour;
    float spawnTime;

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectivesParent.alpha = 0;
        objectivesPanel.alpha = 0;
        abortingParent.alpha = 0;
        abortingProgress.fillAmount = 0;
        objectivesPanel.transform.SetParent(transform);
        objectivePref.gameObject.SetActive(false);
        startColour = objectivePref.color;
        foreach (Image i in objectivePref.GetComponentsInChildren<Image>())
            if (i.name == "Progress Bar")
                startProgressColour = i.color;
        spawnTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= spawnTime + 1.2f)
            objectivesPanel.alpha = Mathf.Lerp(objectivesPanel.alpha, 1, 0.005f);
        if (Time.timeSinceLevelLoad >= spawnTime + 1.8f)
        {
            objectivesParent.alpha = Mathf.Lerp(objectivesParent.alpha, 1, 0.05f);
            if (GameManager.me.aborting)
            {
                abortingParent.alpha = Mathf.Lerp(abortingParent.alpha, 1, 0.05f);
                abortingProgress.fillAmount = Mathf.Lerp(abortingProgress.fillAmount, 1, 0.01f);
            }
            else
            {
                abortingParent.alpha = Mathf.Lerp(abortingParent.alpha, 0, 0.05f);
                abortingProgress.fillAmount = Mathf.Lerp(abortingProgress.fillAmount, 0, 0.05f);
            }
        }
        foreach (ObjectiveSlot slot in slots)
        {
            slot.text.text = "- " + slot.Objective.name;
            slot.progress.transform.parent.gameObject.SetActive(slot.Objective.showProgress);
            slot.progress.fillAmount = Mathf.Lerp((float)slot.progress.fillAmount, (float)slot.Objective.progress / slot.Objective.requiredProgress, 0.1f);
            slot.toggle.gameObject.SetActive(!slot.Objective.showProgress);
            slot.toggle.isOn = slot.Objective.completed;
            slot.toggle.interactable = slot.Objective.completed;
            slot.text.GetComponent<Animator>().enabled = slot.Objective == ObjectiveManager.me.currentObjective;
            if (slot.Objective == ObjectiveManager.me.currentObjective)
            {
                slot.progress.transform.parent.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(slot.progress.transform.parent.GetComponent<CanvasGroup>().alpha, 1, 0.1f);
                slot.text.color = Color.Lerp(slot.text.color, startColour, 0.1f);
                slot.progress.color = Color.Lerp(slot.progress.color, startProgressColour, 0.1f);
            }
            else
            {
                slot.progress.transform.parent.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(slot.progress.transform.parent.GetComponent<CanvasGroup>().alpha, 0.2f, 0.1f);
                slot.text.color = Color.Lerp(slot.text.color, disabledColour, 0.1f);
                slot.progress.color = Color.Lerp(slot.progress.color, disabledProgressColour, 0.1f);
            }
        }
    }

    public void AddObjective(Objective objective)
    {
        bool pass = true;
        foreach (ObjectiveSlot slot in slots)
            if (slot.Objective ==  objective)
                pass = false;
        if (pass)
        {
            TextMeshProUGUI text = Instantiate(objectivePref, objectivePref.transform.parent);
            text.transform.SetAsFirstSibling();
            Image progress = null;
            Toggle toggle = null;
            foreach (Transform i in text.GetComponentsInChildren<Transform>())
            {
                if (i.name == "Progress Bar")
                    progress = i.GetComponent<Image>();
                if (i.name == "Toggle")
                    toggle = i.GetComponent<Toggle>();          
            }
            progress.fillAmount = 0;
            text.gameObject.SetActive(true);
            slots.Add(new ObjectiveSlot { Objective = objective, text = text, progress = progress, toggle = toggle } );
        }
    }
}
