using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public Dude dudePrefab;

    private Dude dude;

    // Start is called before the first frame update
    void Start()
    {
        AddDude();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && dude)
        {
            dude.Launch();
            dude = null;
            Invoke("AddDude", 1f);
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync("Main");
    }

    void AddDude()
    {
        dude = Instantiate(dudePrefab, Vector3.zero, Quaternion.identity);
    }
}
