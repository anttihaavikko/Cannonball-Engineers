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
    public List<Door> moreDoors;
    public GameObject wire;
    public List<Gear> gears;
    public int angle;
    public float moveTime = 1.5f;
    public bool multipleActivations;

    private Vector3 startPos;
    private int activations;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Activate()
    {
        activations++;

        icons.ToList().ForEach(i =>
        {
            i.color = Color.white;
            EffectManager.Instance.AddEffect(3, i.transform.position);
        });

        if (wire)
            wire.SetActive(true);

        float rotMulti = multipleActivations ? activations : 1f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * rotMulti), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Open();
            return;
        }

        if(moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Open());
        }

        if(angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos + direction, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }

    public void Deactivate()
    {
        activations--;

        icons.ToList().ForEach(i => i.color = new Color(0.25f, 0.25f, 0.25f));

        if(wire)
            wire.SetActive(false);

        float rotMulti = multipleActivations ? activations : 0f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * rotMulti), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Close();
            return;
        }

        if (moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Close());
        }

        if (angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos, 1.5f, 0f, TweenEasings.LinearInterpolation);
    }

    void DoRotation()
    {
        Tweener.Instance.RotateBodyTo(body, Quaternion.Euler(0, 0, angle * activations), moveTime - 0.25f, 0f, TweenEasings.LinearInterpolation);
    }
}
