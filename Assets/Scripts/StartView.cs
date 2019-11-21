using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    public EffectCamera cam;
    public Blinders blinders;

    // Start is called before the first frame update
    void Start()
    {
        //Glitch();
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            blinders.Close();
            Invoke("ToLevelSelect", blinders.GetDuration());
        } 
    }

    void Glitch()
    {
        cam.Chromate(0.3f, 1f);
        Invoke("Glitch", 1f);
    }

    void ToLevelSelect()
    {
        SceneManager.LoadSceneAsync("Levels");
    }
}
