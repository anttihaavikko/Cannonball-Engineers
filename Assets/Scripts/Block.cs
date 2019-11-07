using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody2D body;
    public SpriteRenderer[] icons;
    public Door door;
    public GameObject wire;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Activate()
    {
        icons.ToList().ForEach(i =>
        {
            i.color = Color.white;
            EffectManager.Instance.AddEffect(3, i.transform.position);
        });

        if (wire)
            wire.SetActive(true);

        if (door)
        {
            door.Open();
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos + direction, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }

    public void Deactivate()
    {
        icons.ToList().ForEach(i => i.color = new Color(0.25f, 0.25f, 0.25f));

        if(wire)
            wire.SetActive(false);

        if (door)
        {
            door.Close();
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }
}
