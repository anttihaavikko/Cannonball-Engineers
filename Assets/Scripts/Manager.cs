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

    public List<string> levels;

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
        if (levelToActivate >= levels.Count) levelToActivate = 0;
    }
}
