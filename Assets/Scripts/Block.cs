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
    public bool eachActivates;
    public bool isActivator;

    private Vector3 startPos;
    private int activations;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isActivator && collision.gameObject.tag == "ActivationArea")
        {
            Activate(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActivator && collision.gameObject.tag == "ActivationArea")
        {
            Deactivate(true);
        }
    }

    public void Activate(bool forced = false)
    {
        if (isActivator && !forced) return;

        activations++;

        var lightNum = 0;
        icons.ToList().ForEach(i =>
        {

            if(!eachActivates || lightNum < activations)
            {
                i.color = Color.white;
                EffectManager.Instance.AddEffect(3, i.transform.position);
            }

            lightNum++;
        });

        if (wire)
            wire.SetActive(true);

        float multi = multipleActivations || eachActivates ? activations : 1f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * multi), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Open(moveTime);
            return;
        }

        if(moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Open(moveTime));
        }

        if(angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos + direction * multi, moveTime, 0f, TweenEasings.LinearInterpolation);
    }

    public void Deactivate(bool forced = false)
    {
        if (isActivator && !forced) return;

        activations--;

        icons.ToList().ForEach(i => i.color = new Color(0.25f, 0.25f, 0.25f));

        if(wire)
            wire.SetActive(false);

        float rotMulti = multipleActivations ? activations : 0f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * rotMulti), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Close(moveTime * 0.5f);
            return;
        }

        if (moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Close(moveTime));
        }

        if (angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos, moveTime, 0f, TweenEasings.LinearInterpolation);
    }

    void DoRotation()
    {
        Tweener.Instance.RotateBodyTo(body, Quaternion.Euler(0, 0, angle * activations), moveTime - 0.25f, 0f, TweenEasings.LinearInterpolation);
    }
}
