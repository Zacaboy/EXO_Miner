using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButton : MonoBehaviour
{
    // Variables
    public AudioClip clickSFX;

    // Ignore
    TextMeshProUGUI text;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the variables
        text = GetComponentInChildren<TextMeshProUGUI>();
        source = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Assigns the data
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
