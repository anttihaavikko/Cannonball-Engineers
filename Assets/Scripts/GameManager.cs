using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SuccessView successView;
    public string time;
    public float timeAmount;
    public bool running = true;
	public int starCount = 3;
	public int deaths;
    public bool deathPunished;
    public bool isIntro;

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

    private void Start()
    {
        SceneChanger.Instance.AttachCamera();

        Manager.Instance.isHoveringSomething = false;

		AudioManager.Instance.BackToDefaultMusic();
	}

    private void Update()
    {
        if (isIntro) return;

        if (Input.GetKeyDown(KeyCode.R))
            Restart();

        if (Input.GetKeyUp(KeyCode.Escape))
            BackToLevelSelect();
    }

    public void Success()
    {
        if(successView)
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
        if(Manager.Instance.levelToActivate == 0)
        {
            SceneChanger.Instance.ChangeScene("End");
        }
        else
        {
            SceneChanger.Instance.ChangeScene("Main");
        }
    }
}
