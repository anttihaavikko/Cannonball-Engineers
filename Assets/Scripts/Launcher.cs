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

    public bool immortals;
    public bool jumpers;

    private Dude dude, reserveDude;
    private Vector3 launcherPos;
    private bool hasReserve;

    // Start is called before the first frame update
    void Start()
    {
        AddDude();
        launcherPos = launcher.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dude)
        {
            dude.UpdateLine();
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = pos - dude.body.transform.position;

            var angle = Mathf.Clamp(dir.x, -45f, 45f);

            torquePointer.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetMouseButtonUp(0) && dude)
        {
            dude.Launch(followCam);

            if (!hasReserve)
            {
                Invoke("AddDude", 2f);
            } else
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

    void AddDude()
    {
        if (hasReserve) return;

        reserveDude = Instantiate(dudePrefab, transform.position, Quaternion.identity);
        reserveDude.launcher = this;
        reserveDude.jumper = jumpers;
        reserveDude.canDie = immortals;
        reserveDude.NudgeHands();
        reserveDude.line.enabled = false;
        hasReserve = true;

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

        dude = reserveDude;

        if (dude)
            dude.line.enabled = true;

        hasReserve = false;
    }
}
