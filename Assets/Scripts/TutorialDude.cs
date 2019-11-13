using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDude : MonoBehaviour
{
    public SpeechBubble bubble;
    public GameObject zoomCam;
    public Animator anim;

    private bool showing;
    private string nextMessage;
    private bool appeared;

    private static TutorialDude instance = null;
    public static TutorialDude Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(appeared && showing && Input.anyKey)
        {
            zoomCam.SetActive(false);
            bubble.Hide();
            Invoke("AfterHide", 0.5f);
        }
    }

    public void Show(string message, float delay = 0f)
    {
        nextMessage = message;
        showing = true;
        Invoke("IntroMessage", delay + 0.7f);
        Invoke("ZoomIn", delay);
    }

    void IntroMessage()
    {
        ShowMessage(nextMessage);
    }

    void ShowMessage(string message)
    {
        appeared = true;
        bubble.ShowMessage(message);
        anim.SetBool("pointing", true);
    }

    void ZoomIn()
    {
        showing = true;
        zoomCam.SetActive(true);
    }

    public bool IsShowing()
    {
        return showing;
    }

    void AfterHide()
    {
        appeared = false;
        showing = false;
        anim.SetBool("pointing", false);
    }
}
