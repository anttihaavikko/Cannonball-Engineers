using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Blinders blinders;
    public Transform spinner;
    public string sceneToLoadAtStart;

    private string sceneToLoad;
    private AsyncOperation operation;

    private static SceneChanger instance = null;
    public static SceneChanger Instance
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

        DontDestroyOnLoad(instance.gameObject);
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(sceneToLoadAtStart))
            ChangeScene(sceneToLoadAtStart);
    }

    private void Update()
    {
        if(operation != null && operation.isDone)
        {
            blinders.Open();
            Tweener.Instance.ScaleTo(spinner, Vector3.zero, 0.2f, 0f, TweenEasings.QuadraticEaseIn);
            operation = null;
        }
    }

    public void ChangeScene(string sceneName)
    {
        blinders.Close();
        Tweener.Instance.ScaleTo(spinner, Vector3.one, 0.2f, 0f, TweenEasings.BounceEaseOut);
        sceneToLoad = sceneName;
        Invoke("DoChangeScene", blinders.GetDuration());
    }

    void DoChangeScene()
    {
        operation = SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
