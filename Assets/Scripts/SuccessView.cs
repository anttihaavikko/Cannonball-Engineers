using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuccessView : MonoBehaviour
{
    public List<Toggler> togglables;
    public Blinders dimmers;
    public List<Star> stars;
    public TMPro.TextMeshProUGUI timeText;

    public void Appear()
    {
        timeText.text = GameManager.Instance.time;
        dimmers.Close();
        Invoke("ShowTogglers", 0.6f);

        Manager.Instance.LevelCompleted();
    }

    void ShowTogglers()
    {
        togglables.ForEach(t => t.Show());
        Invoke("ShowStars", 3f);
    }

    void ShowStars()
    {
        for(int i = 0; i < GameManager.Instance.starCount; i++)
        {
            stars[i].Appear();
        }
    }
}
