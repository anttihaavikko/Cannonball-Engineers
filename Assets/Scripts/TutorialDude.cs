using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDude : MonoBehaviour
{
    public SpeechBubble bubble;
    public GameObject zoomCam;
    public Animator anim;

    private bool showing;

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

    // Start is called before the first frame update
    void Start()
    {
        showing = true;
        Invoke("IntroMessage", 1f);
        Invoke("ZoomIn", 0.3f);
    }

    private void Update()
    {
        if(showing && Input.anyKey)
        {
            zoomCam.SetActive(false);
            bubble.Hide();
            Invoke("AfterHide", 0.5f);
        }
    }

    void IntroMessage()
    {
        //ShowMessage("Oh no, we've ran out of hard hats! Be very careful!");
        ShowMessage("You can command any engineer you want! Just point at them to give orders!");
    }

    void ShowMessage(string message)
    {
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
        showing = false;
        anim.SetBool("pointing", false);
    }
}
