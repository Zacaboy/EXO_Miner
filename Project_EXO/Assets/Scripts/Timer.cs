using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer me;
    public int maxTime = 15;
    public int bonusTime = 10;
    public bool noLimit;
    [HideInInspector] public float time;
    float lastTime;
    bool failed;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        if (!noLimit)
            time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastTime + 60 & !failed)
        {
            lastTime = Time.time;
            if (!noLimit)
                time -= 1;
            else
                time += 1;
            CheckTime();
        }
        if (time <= 0)
            time = 0;
    }

    public void CheckTime()
    {
        if (noLimit) return;
        if (time <= 0)
        {
            GameManager.me.FailObjective();
            failed = true;
        }
    }
}
