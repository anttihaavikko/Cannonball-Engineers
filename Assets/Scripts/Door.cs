using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody2D body;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Open(float duration = 1.5f)
    {
        Tweener.Instance.MoveBodyTo(body, startPos + direction, duration, 0f, TweenEasings.LinearInterpolation);
        DoSounds(duration);
    }

    public void Close(float duration = 1.5f)
    {
        Tweener.Instance.MoveBodyTo(body, startPos, duration, 0f, TweenEasings.LinearInterpolation);
        DoSounds(duration);
    }

    void DoSounds(float duration)
    {
        CancelInvoke("Clang");
        CancelInvoke("DoSound");

        Invoke("Clang", duration);

        for (float d = 0f; d < duration - 1f; d += 1.3f)
            Invoke("DoSound", d);
    }

    void DoSound()
    {
        AudioManager.Instance.PlayEffectAt(23, transform.position, 0.747f);
        AudioManager.Instance.PlayEffectAt(24, transform.position, 0.796f);
        AudioManager.Instance.PlayEffectAt(37, transform.position, 1f);
    }

    void Clang()
    {
        AudioManager.Instance.PlayEffectAt(1, transform.position, 0.579f);
        AudioManager.Instance.PlayEffectAt(11, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(17, transform.position, 0.705f);
        AudioManager.Instance.PlayEffectAt(16, transform.position, 0.587f);
        AudioManager.Instance.PlayEffectAt(39, transform.position, 1.12f);
    }
}
