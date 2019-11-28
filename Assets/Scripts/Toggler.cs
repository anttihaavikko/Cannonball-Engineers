using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    public bool startsHidden = true;
    public Vector3 hiddenSize = Vector3.zero;
    public float duration = 0.2f;
    public float showDelay, hideDelay;
    public EffectCamera effectCamera;
    public bool doSound;
    public float volume = 1f;

    private Vector3 shownSize;

    // Start is called before the first frame update
    void Start()
    {
        shownSize = transform.localScale;
        if (startsHidden) transform.localScale = hiddenSize;
    }

    public void Show()
    {
        Tweener.Instance.ScaleTo(transform, shownSize, duration, showDelay, TweenEasings.BounceEaseOut);
        Invoke("DoEffect", showDelay + duration * 0.8f);

        if (doSound)
            Invoke("DoSound", showDelay * 1.1f);
    }

    void DoSound()
    {
        AudioManager.Instance.PlayEffectAt(1, transform.position, 1.285f * volume);
        AudioManager.Instance.PlayEffectAt(4, transform.position, 1.653f * volume);
        AudioManager.Instance.PlayEffectAt(6, transform.position, 1.828f * volume);
        AudioManager.Instance.PlayEffectAt(10, transform.position, 1.4f * volume);
        AudioManager.Instance.PlayEffectAt(18, transform.position, 0.876f * volume);
    }

    public void Hide()
    {
        Tweener.Instance.ScaleTo(transform, hiddenSize, duration * 0.5f, hideDelay, TweenEasings.QuadraticEaseOut);
    }

    // Update is called once per frame
    void DoEffect()
    {
        if (effectCamera)
        {
            effectCamera.BaseEffect(0.2f);
        }
    }
}
