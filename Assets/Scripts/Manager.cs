﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Manager : MonoBehaviour {

	public Dude hoveredDude;
	public Dude activeDude;
	public int levelToActivate = -1;
	public List<LevelData> levelData;
	public float levelListPosition;
	public bool isHoveringSomething;

    public bool hasDoneTorque, hasSeenSnap, hasBashedHead, hasOverlapped;

	public static string[] levels = {
        "Hello World",
        "Double",
        "Doors",
        "Lost Delivery",
        "Bends",
        "The Vault",
        "Hatch",
        "Ferris Wheel",
        "Maze",
        "Hangman",
        "Teamwork",
        "Oil Spill",
        "Suicide Mission",
        "Swinger",
        "Time to Crate",
        "Center of Mass",
        "Swinging Activator",
        "Strange Attractor",
        "Tricky Crate",
        "Long Mover",
        "Trickier Crate",
        "Buffer Overflow",
        "Null Pointer",
        "Doomsday Source"
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

        levelData = new List<LevelData>();

        LoadData();

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

        if(levelData.Any(l => l.name == level))
        {
            var lv = levelData.Find(l => l.name == level);
            lv.AddTime(time);
            lv.AddStars(stars);
        }
        else
        {
            Debug.Log("Adding scores for " + level);
            levelData.Add(new LevelData
            {
                name = level,
                time = time,
                stars = stars
            });
        }

        SaveData();
    }

    public static string TimeToString(float time)
    {
        var total = Mathf.FloorToInt(time);
        var minutes = total / 60;
        var seconds = total % 60;
        return $"{minutes}:{seconds.ToString("D2")}";
    }

    void SaveData()
    {
        var data = new SaveData()
        {
            levels = levelData.ToArray(),
            hasDoneTorque = hasDoneTorque,
            hasSeenSnap = hasSeenSnap,
            hasBashedHead = hasBashedHead,
            hasOverlapped = hasOverlapped
    };

        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Data", json);
    }

    void LoadData()
    {
        if(PlayerPrefs.HasKey("Data"))
        {
            var json = PlayerPrefs.GetString("Data");
            var data = JsonUtility.FromJson<SaveData>(json);
            levelData = data.levels.ToList();
            hasDoneTorque = data.hasDoneTorque;
            hasSeenSnap = data.hasSeenSnap;
            hasBashedHead = data.hasBashedHead;
            hasOverlapped = data.hasOverlapped;
        }
    }
}

[Serializable]
public class SaveData
{
    public LevelData[] levels;
    public bool hasDoneTorque, hasSeenSnap, hasBashedHead, hasOverlapped;
}

[Serializable]
public class LevelData
{
    public string name;
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