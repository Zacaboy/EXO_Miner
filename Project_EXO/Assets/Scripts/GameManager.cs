using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager me;

    [Header("Effects")]
    public UIMessageScript startEffect;
    public UIMessageScript endEffect;

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
        UIMessageScript effect = Instantiate(startEffect);
    }

    public void CompleteObjective()
    {
        if (over) return;
        over = true;
        completeEvent.Invoke();
        StartCoroutine(WaitFinish(2));
    }

    public void FailObjective()
    {
        if (over) return;
        over = true;
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
