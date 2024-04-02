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

    [Header("Other")]
    public AnimationCurve fadeIn;

    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public UnityEvent completeEvent;
    [HideInInspector] public UnityEvent failEvent;
    [HideInInspector] public bool over;

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

    public void CompleteObjective()
    {
        if (over) return;
        over = true;
        UIMessageScript ui = Instantiate(winUI);
        completeEvent.Invoke();
        StartCoroutine(WaitFinish(2));
    }

    public void FailObjective(bool dead)
    {
        if (over) return;
        over = true;
        if (dead)
        {
            UIMessageScript ui = Instantiate(deathUI);
        }
        else
        {
            UIMessageScript ui = Instantiate(failUI);
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
            SceneManager.LoadScene("Main Menu");
        }
    }
}
