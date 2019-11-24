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
    public TMPro.TextMeshPro threeLimit, twoLimit, timer;

    public int threeStarLimit = 3;
    public int twoStarLimit = 5;
    public bool punishDeaths;

    public bool immortals;
    public bool jumpers;

    private Dude reserveDude;
    private Vector3 launcherPos;
    private bool hasReserve;

    private List<Dude> dudes;
    private int launchCount;
    private float levelTime;

    // Start is called before the first frame update
    void Start()
    {
        dudes = new List<Dude>();

        UpdateCounter();

        AddDude();
        launcherPos = launcher.transform.position;

        var p = TutorialDude.Instance.transform.position;
        TutorialDude.Instance.transform.position = new Vector3(tutorialSpot.position.x, p.y, p.z);

        if(introMessage != null && introMessage != string.Empty)
        {
            TutorialDude.Instance.Show(introMessage, 0.3f);
        }

        GameManager.Instance.deathPunished = punishDeaths;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.running) {
            levelTime += Time.deltaTime;

            var total = Mathf.FloorToInt(levelTime);
            var minutes = total / 60;

            timer.text = Manager.TimeToString(levelTime);

            if (minutes >= 10)
            {
                timer.fontSize = 4f;
            }

            GameManager.Instance.time = timer.text;
            GameManager.Instance.timeAmount = levelTime;
        }

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

            var wasLaunch = dude.Launch(followCam);

            CancelInvoke("AddDude");
            Invoke("AddDude", 2f);

            if (hasReserve)
            {
                hasReserve = Manager.Instance.activeDude != reserveDude;
            }

            if (!wasLaunch) return;

            launchCount++;

            UpdateCounter();

            hasReserve = false;

            EffectManager.Instance.AddEffect(4, transform.position);
            EffectManager.Instance.AddEffect(7, transform.position);

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = pos - dude.body.transform.position;
            var amt = Mathf.Min(Mathf.Max(0, dir.y), 50f);
            var speed = (50f - amt) / 50f;

            Tweener.Instance.MoveTo(launcher, launcherPos + Vector3.up * amt * 0.2f, 0.1f + speed * 0.1f, 0f, TweenEasings.BounceEaseInOut);
            Invoke("ResetPos", 0.75f);

            //dude = null;
        }
    }

    void UpdateCounter()
    {
        threeLimit.text = Mathf.Max(threeStarLimit - launchCount, 0).ToString();
        twoLimit.text = Mathf.Max(twoStarLimit - launchCount, 0).ToString();

        if (launchCount > threeStarLimit) GameManager.Instance.starCount = 2;
        if (launchCount > twoStarLimit) GameManager.Instance.starCount = 1;
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

        reserveDude.gameObject.name = "Dude #" + launchCount;

        dudes.Add(reserveDude);
        GarbageCollection();

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
    }

    public void ActivateIfReserve(Dude d)
    {
        if(d == reserveDude)
        {
            ActivateReserve();
        }
    }

    void GarbageCollection()
    {
        int limit = 10;
        var deads = dudes.FindAll(d => d != null && !d.isAlive);

        if (dudes.Count > limit)
            Invoke("GarbageCollection", 5f);

        if (dudes.Count > limit && deads.Count > 0)
        {
            var toRemove = dudes.Find(d => !d.isAlive);
            dudes.Remove(toRemove);
            Destroy(toRemove.gameObject);
        }

        if(dudes.Count > 15)
        {
            var d = dudes[0];
            d.Die();
            Invoke("RemoveFirstDude", 5f);
        }
    }

    void RemoveFirstDude()
    {
        Destroy(dudes[0].gameObject);
        dudes.RemoveAt(0);
    }
}
