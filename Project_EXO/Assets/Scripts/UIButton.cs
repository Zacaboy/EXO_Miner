using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButton : MonoBehaviour
{
    public AudioClip clickSFX;
    TextMeshProUGUI text;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = name;
    }

    public void PlaySFX()
    {
        source.PlayOneShot(clickSFX);
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(UIControl.me.WaitLoad(level));
    }

    public void Exit()
    {
        Application.Quit();
    }
}
