using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public Rotator rotator;

    private Vector3 normalSize;
    private float normalSpeed;
    private float speedMulti = 1f;
    private float prevX;
    private float direction;
    private float limit = 1f;

    // Start is called before the first frame update
    void Start()
    {
        normalSize = transform.localScale;
        normalSpeed = rotator.speed;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (transform.position.x - prevX < -limit) direction = -1f;
        if (transform.position.x - prevX > limit) direction = 1f;
        rotator.speed = Mathf.MoveTowards(rotator.speed, normalSpeed * speedMulti * direction, Time.deltaTime * 30f);
        prevX = transform.position.x;
    }

    public void Grow()
    {
        speedMulti = 2f;
        Tweener.Instance.ScaleTo(transform, normalSize * 1.5f, 0.25f, 0f, TweenEasings.BounceEaseOut);
        Cursor.visible = false;
    }

    public void Normalize()
    {
        speedMulti = 1f;
        Tweener.Instance.ScaleTo(transform, normalSize, 0.1f, 0f, TweenEasings.QuadraticEaseInOut);
        Cursor.visible = false;
    }

    public void Shrink()
    {
        speedMulti = 2f;
        Tweener.Instance.ScaleTo(transform, normalSize * 0.6f, 0.1f, 0f, TweenEasings.QuadraticEaseInOut);
        Cursor.visible = false;
    }
}
