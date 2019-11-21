using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    public EffectCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        //Glitch();
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneChanger.Instance.ChangeScene("Levels");
        } 
    }

    void Glitch()
    {
        cam.Chromate(0.3f, 1f);
        Invoke("Glitch", 1f);
    }

}
