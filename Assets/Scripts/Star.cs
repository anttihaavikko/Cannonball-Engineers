using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Transform filling, pulse;
    public Image pulseSprite;
    public float delay;

    // Start is called before the first frame update
    public void Appear()
    {
        float dur = 0.33f;
        Tweener.Instance.ScaleTo(filling, Vector3.one, dur, delay, TweenEasings.BounceEaseOut);
        Invoke("StartPulse", delay + dur);
    }

    void StartPulse()
    {
        pulse.localScale = Vector3.one;
        Tweener.Instance.ScaleTo(pulse, Vector3.one * 2.5f, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
        Tweener.Instance.ColorTo(pulseSprite, Color.clear, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
    }
}
