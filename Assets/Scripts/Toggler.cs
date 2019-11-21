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
    }

    public void Hide()
    {
        Tweener.Instance.ScaleTo(transform, shownSize, duration, showDelay, TweenEasings.QuadraticEaseIn);
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
