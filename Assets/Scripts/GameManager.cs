﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SuccessView successView;
    public string time;
    public bool running = true;

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

        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
    }

    public void Success()
    {
        successView.Appear();
    }

    public void BackToLevelSelect()
    {
        SceneChanger.Instance.ChangeScene("Levels");
    }

    public void Restart()
    {
        SceneChanger.Instance.ChangeScene("Main");
    }

    public void NextLevel()
    {
        Manager.Instance.NextLevel();
        SceneChanger.Instance.ChangeScene("Main");
    }
}
