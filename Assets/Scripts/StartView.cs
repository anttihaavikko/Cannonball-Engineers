using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    public EffectCamera cam;
    public Toggler toggler;

    // Start is called before the first frame update
    void Start()
    {
        //Glitch();
        toggler.Show();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            SceneChanger.Instance.blinders.Close();
            Invoke("Quit", SceneChanger.Instance.blinders.GetDuration() + 0.2f);
        }
            
        if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneChanger.Instance.ChangeScene("Levels");
        } 
    }

    void Glitch()
    {
        cam.Chromate(0.3f, 1f);
        Invoke("Glitch", 1f);
    }

    void Quit()
    {
        Application.Quit();
    }
}
