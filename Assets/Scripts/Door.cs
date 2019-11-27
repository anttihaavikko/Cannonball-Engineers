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
    }

    public void Close(float duration = 1.5f)
    {
        Tweener.Instance.MoveBodyTo(body, startPos, duration, 0f, TweenEasings.LinearInterpolation);
    }
}
