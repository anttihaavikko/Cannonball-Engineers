using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public Dude dudePrefab;
    public Transform launcher;
    public Cinemachine.CinemachineVirtualCamera followCam;
    public Transform torquePointer;
    public Transform tutorialSpot;
    public string introMessage;

    public bool immortals;
    public bool jumpers;

    private Dude reserveDude;
    private Vector3 launcherPos;
    private bool hasReserve;

    // Start is called before the first frame update
    void Start()
    {
        AddDude();
        launcherPos = launcher.transform.position;

        var p = TutorialDude.Instance.transform.position;
        TutorialDude.Instance.transform.position = new Vector3(tutorialSpot.position.x, p.y, p.z);

        if(introMessage != null && introMessage != string.Empty)
        {
            TutorialDude.Instance.Show(introMessage, 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dude = Manager.Instance.activeDude;

        if (dude)
        {
            dude.UpdateLine();
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = pos - dude.body.transform.position;

            var angle = Mathf.Clamp(dir.x, -45f, 45f);

            torquePointer.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetMouseButtonUp(0) && dude && !TutorialDude.Instance.IsShowing())
        {
            if (Manager.Instance.hoveredDude)
            {
                return;
            }

            dude.Launch(followCam);

            Invoke("AddDude", 2f);

            if (hasReserve)
            {
                return;
            }

            EffectManager.Instance.AddEffect(4, transform.position);

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = pos - dude.body.transform.position;
            var amt = Mathf.Min(Mathf.Max(0, dir.y), 50f);
            var speed = (50f - amt) / 50f;

            Tweener.Instance.MoveTo(launcher, launcherPos + Vector3.up * amt * 0.2f, 0.1f + speed * 0.1f, 0f, TweenEasings.BounceEaseInOut);
            Invoke("ResetPos", 0.75f);

            //dude = null;
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync("Main");
    }

    public void AddDude()
    {
        if (hasReserve)
        {
            ActivateIfNeeded();
            return;
        }

        reserveDude = Instantiate(dudePrefab, transform.position, Quaternion.identity);
        reserveDude.launcher = this;
        reserveDude.jumper = jumpers;
        reserveDude.canDie = immortals;
        reserveDude.AddHardHat(immortals);
        reserveDude.NudgeHands();
        reserveDude.line.enabled = false;
        hasReserve = true;

        ActivateIfNeeded();
    }

    public void ActivateIfNeeded()
    {
        var dude = Manager.Instance.activeDude;

        if (dude == null || !dude.isAlive || dude.IsDone())
        {
            ActivateReserve();
        }
    }

    void ResetPos()
    {
        Tweener.Instance.MoveTo(launcher, launcherPos, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
    }

    public void ActivateReserve()
    {
        if (!hasReserve)
            return;

        Manager.Instance.activeDude = reserveDude;

        if (Manager.Instance.activeDude)
        {
            Manager.Instance.activeDude.line.enabled = true;
        }

        hasReserve = false;
    }

    public void ActivateIfReserve(Dude d)
    {
        if(d == reserveDude)
        {
            ActivateReserve();
        }
    }
}
