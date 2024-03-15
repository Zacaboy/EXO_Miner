using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageScript : MonoBehaviour
{
    CanvasGroup canvas;
    public float flashTime = 3;
    public float startAlpha;
    public float inRate = 0.1f;
    public float outRate = 0.05f;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = startAlpha;
        time = Time.fixedTime;
        Destroy(gameObject, flashTime + 5);
    }

    void FixedUpdate()
    {
        if (Time.fixedTime < time + flashTime)
            canvas.alpha = Mathf.Lerp(canvas.alpha, 1, inRate);
        else
            canvas.alpha = Mathf.Lerp(canvas.alpha, 0, outRate);
    }
}
