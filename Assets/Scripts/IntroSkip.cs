using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSkip : MonoBehaviour
{
    public Vector3 hiddenSize = Vector3.zero;
    public float speed = 0.3f;

    private Vector3 targetSize;
    private float escHeldFor;
    private bool doing;

    // Start is called before the first frame update
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = hiddenSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (doing) return;

        if (Input.anyKeyDown)
        {
            Tweener.Instance.ScaleTo(transform, targetSize, speed, 0f, TweenEasings.BounceEaseOut);
            DoSound();
        }

        if (!Input.anyKey)
        {
            escHeldFor = 0f;
        }

        if (Input.anyKey)
        {
            escHeldFor += Time.deltaTime;
            CancelInvoke("HideText");
            Invoke("HideText", 2f);
        }

        if(escHeldFor > 0.5f)
        {
            doing = true;
            SceneChanger.Instance.ChangeScene("Start");
        }
    }

    void HideText()
    {
        Tweener.Instance.ScaleTo(transform, hiddenSize, speed, 0f, TweenEasings.QuarticEaseIn);
        DoSound();
    }

    void DoSound()
    {
        AudioManager.Instance.PlayEffectAt(25, transform.position, 0.5f);
        AudioManager.Instance.PlayEffectAt(1, transform.position, 0.75f);
    }
}
