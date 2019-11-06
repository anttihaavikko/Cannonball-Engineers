using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public Dude dudePrefab;
    public Transform launcher;

    private Dude dude;
    private Vector3 launcherPos;

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
            dude.UpdateLine();

        if (Input.GetMouseButtonUp(0) && dude)
        {
            dude.Launch();
            Invoke("AddDude", 2f);

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = pos - dude.body.transform.position;
            var amt = Mathf.Min(Mathf.Max(0, dir.y), 50f);
            var speed = (50f - amt) / 50f;

            Tweener.Instance.MoveTo(launcher, launcherPos + Vector3.up * amt * 0.2f, 0.3f + speed * 0.1f, 0f, TweenEasings.BounceEaseInOut);
            Invoke("ResetPos", 0.75f);

            dude = null;
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync("Main");
    }

    void AddDude()
    {
        dude = Instantiate(dudePrefab, Vector3.zero, Quaternion.identity);
    }

    void ResetPos()
    {
        Tweener.Instance.MoveTo(launcher, launcherPos, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
    }
}
