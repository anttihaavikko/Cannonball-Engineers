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
        Tweener.Instance.RotateTo(filling, Quaternion.Euler(0, 0, 0), dur * 0.5f, delay, TweenEasings.BounceEaseOut);
        Invoke("StartPulse", delay + dur);

        Invoke("DoSound", delay + dur * 1.05f);
    }

    void DoSound()
    {
        AudioManager.Instance.PlayEffectAt(2, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(3, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(5, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(0, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 1f);
    }

    void StartPulse()
    {
        pulse.localScale = Vector3.one;
        Tweener.Instance.ScaleTo(pulse, Vector3.one * 2.5f, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
        Tweener.Instance.ColorTo(pulseSprite, Color.clear, 0.3f, 0f, TweenEasings.QuadraticEaseIn);
    }
}
