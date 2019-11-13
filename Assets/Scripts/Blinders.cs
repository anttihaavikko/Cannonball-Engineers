using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinders : MonoBehaviour
{
    public Transform left, right;

    private float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        left.transform.localScale = new Vector3(1f, 1f, 1f);
        right.transform.localScale = new Vector3(1f, 1f, 1f);
        Invoke("Open", 0.3f);
    }

    public void Close()
    {
        Tweener.Instance.ScaleTo(left, Vector3.one, duration, 0f, TweenEasings.BounceEaseOut);
        Tweener.Instance.ScaleTo(right, Vector3.one, duration, 0f, TweenEasings.BounceEaseOut);
    }

    public void Open()
    {
        Tweener.Instance.ScaleTo(left, new Vector3(0f, 1f, 1f), duration, 0f, TweenEasings.BounceEaseOut);
        Tweener.Instance.ScaleTo(right, new Vector3(0f, 1f, 1f), duration, 0f, TweenEasings.BounceEaseOut);
    }

    public float GetDuration()
    {
        return duration;
    }
}
