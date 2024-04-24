using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager me;

    [Header("Map")]
    public Sprite mapIcon;

    [Header("Effects")]
    public UIMessageScript startEffect;
    public UIMessageScript endEffect;
    public UIMessageScript winUI;
    public UIMessageScript deathUI;
    public UIMessageScript failUI;
    public UIMessageScript abortUI;  

    [Header("Other")]
    public AnimationCurve fadeIn;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public UnityEvent completeEvent;
    [HideInInspector] public UnityEvent failEvent;
    [HideInInspector] public bool over;
    [HideInInspector] public bool aborting;
    [HideInInspector] public float exitTimer;

    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = PlayerMechController.me.transform.position;
        UIMessageScript effect = Instantiate(startEffect);
    }

    // Update is called once per frame
    void Update()
    {
        if (over)
            aborting = false;
        if (!over & Time.timeSinceLevelLoad > 1)
        {
            if (!aborting)
                exitTimer = Time.time;
            if (Time.time >= exitTimer + 3)
                AbortObjective();
        }
        else
            exitTimer = Time.time;
    }

    public void CompleteObjective()
    {
        if (over) return;
        over = true;
        UIMessageScript ui = Instantiate(winUI);
        if (ObjectiveUI.me)
        {
            ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
            ui.transform.position = ObjectiveUI.me.objectivesPanel.transform.position;
            ui.transform.eulerAngles = ObjectiveUI.me.objectivesPanel.transform.eulerAngles;
            ui.transform.localScale = winUI.transform.localScale;
            ui.transform.SetAsFirstSibling();
            ui.transform.localScale = winUI.transform.localScale;
        }
        completeEvent.Invoke();
        StartCoroutine(WaitFinish(2));
    }

    public void AbortObjective()
    {
        if (over) return;
        over = true;
        UIMessageScript ui = Instantiate(abortUI);
        if (ObjectiveUI.me)
        {
            ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
            ui.transform.position = ObjectiveUI.me.objectivesPanel.transform.position;
            ui.transform.eulerAngles = ObjectiveUI.me.objectivesPanel.transform.eulerAngles;
            ui.transform.localScale = winUI.transform.localScale;
            ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
            ui.transform.SetAsFirstSibling();
        }
        failEvent.Invoke();
        StartCoroutine(WaitFinish(2));
    }

    public void FailObjective(bool dead)
    {
        if (over) return;
        over = true;
        if (dead)
        {
            UIMessageScript ui = Instantiate(deathUI);
            if (ObjectiveUI.me)
            {
                ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
                ui.transform.position = ObjectiveUI.me.objectivesPanel.transform.position;
                ui.transform.eulerAngles = ObjectiveUI.me.objectivesPanel.transform.eulerAngles;
                ui.transform.localScale = deathUI.transform.localScale;
                ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
                ui.transform.SetAsFirstSibling();
            }
        }
        else
        {
            UIMessageScript ui = Instantiate(failUI);
            if (ObjectiveUI.me)
            {
                ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
                ui.transform.position = ObjectiveUI.me.objectivesPanel.transform.position;
                ui.transform.eulerAngles = ObjectiveUI.me.objectivesPanel.transform.eulerAngles;
                ui.transform.localScale = failUI.transform.localScale;
                ui.transform.SetParent(ObjectiveUI.me.objectivesPanel.transform);
                ui.transform.SetAsFirstSibling();
            }
        }
        failEvent.Invoke();
        StartCoroutine(WaitFinish(1));
    }

    public IEnumerator WaitFinish(int type)
    {
        yield return new WaitForSeconds(3);
        UIMessageScript effect = Instantiate(endEffect);
        yield return new WaitForSeconds(1);
        if (type == 1)
        {
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (type == 2)
        {
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene("Hub");
        }
    }
}
