using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Manager : MonoBehaviour {

	public Dude hoveredDude;
	public Dude activeDude;
	public int levelToActivate = -1;
	public Dictionary<string, LevelData> levelData;
	public float levelListPosition;
	public bool isHoveringSomething;

	public static string[] levels = {
        "Hellio World",
        "Double",
        "Doors",
        "Lost Delivery",
        "Bends",
        "Hatch",
        "Ferris Wheel",
        "Oil Spill",
        "Maze",
        "Teamwork",
        "Suicide Mission",
        "Swinger",
        "Time to Crate",
        "Center",
        "Swinging Activator",
        "Tricky Crate",
        "Null Pointer"
    };

	private static Manager instance = null;
	public static Manager Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}

        levelData = new Dictionary<string, LevelData>();

        DontDestroyOnLoad(instance.gameObject);
    }

    public void ChangeDude(Dude dude)
    {
        if(activeDude)
        {
            activeDude.line.enabled = false;
        }

        activeDude = dude;
        activeDude.line.enabled = true;
    }

    internal void NextLevel()
    {
        levelToActivate++;
        if (levelToActivate >= levels.Length) levelToActivate = 0;
    }

    public void LevelCompleted()
    {
        if (levelToActivate == -1) return;

        var level = levels[levelToActivate];
        var time = GameManager.Instance.timeAmount;
        var stars = GameManager.Instance.starCount;

        if(levelData.ContainsKey(level))
        {
            Debug.Log("Updating scores for " + level);
            levelData[level].AddTime(time);
            levelData[level].AddStars(stars);
        }
        else
        {
            Debug.Log("Adding scores for " + level);
            levelData.Add(level, new LevelData
            {
                time = time,
                stars = stars
            });
        }
    }

    public static string TimeToString(float time)
    {
        var total = Mathf.FloorToInt(time);
        var minutes = total / 60;
        var seconds = total % 60;
        return $"{minutes}:{seconds.ToString("D2")}";
    }
}

public class LevelData
{
    public float time;
    public int stars;

    public bool IsCompleted()
    {
        return stars > 0;
    }

    public void AddTime(float t)
    {
        time = Mathf.Min(t, time);
    }

    public void AddStars(int s)
    {
        stars = Mathf.Max(stars, s);
    }
}