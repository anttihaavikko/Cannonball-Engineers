using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody2D body;
    public SpriteRenderer[] icons;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Activate()
    {
        icons.ToList().ForEach(i => i.color = Color.white);
        Tweener.Instance.MoveBodyTo(body, startPos + direction, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }

    public void Deactivate()
    {
        icons.ToList().ForEach(i => i.color = new Color(1f, 1f, 1f, 0.25f));
        Tweener.Instance.MoveBodyTo(body, startPos, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }
}
