using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Blinders blinders;
    public SuccessView successView;

    private static GameManager instance = null;
    public static GameManager Instance
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
        if (Input.GetKeyDown(KeyCode.R))
            Restart();

        if (Input.GetKeyDown(KeyCode.Escape))
            BackToLevelSelect();
    }

    public void Success()
    {
        successView.Appear();
    }

    public void BackToLevelSelect()
    {
        blinders.Close();
        Invoke("GoBackToLevelSelect", blinders.GetDuration());
    }

    void GoBackToLevelSelect()
    {
        SceneManager.LoadSceneAsync("Levels");
    }

    public void Restart()
    {
        blinders.Close();
        Invoke("DoRestart", blinders.GetDuration());
    }

    void DoRestart()
    {
        SceneManager.LoadSceneAsync("Main");
    }
}
