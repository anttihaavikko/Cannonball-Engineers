using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MultiBlock : MonoBehaviour
{
    public Rigidbody2D body;
    public List<SpriteRenderer> lights;
    public List<Gear> gears;
    public float size = 7.5f;

    private int[] activations;
    private Vector3 originalPos;
    private Vector3[] directions =
    {
        Vector3.up,
        Vector3.right,
        Vector3.down,
        Vector3.left
    };

    // Start is called before the first frame update
    void Start()
    {
        activations = new int[4];
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        var pos = Vector3.zero;

        for (var i = 0; i < 4; i++)
            pos += activations[i] * directions[i] * size;

        Tweener.Instance.MoveBodyTo(body, originalPos + pos, 1.5f, 0f, TweenEasings.LinearInterpolation);
        Debug.Log(gears.First().transform.rotation.eulerAngles.z);
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount + g.transform.rotation.eulerAngles.z), 1.5f, 0f, TweenEasings.LinearInterpolation));
    }

    public void Activate(string dir)
    {
        switch(dir)
        {
            case "Top":
                Activate(0);
                break;
            case "Right":
                Activate(1);
                break;
            case "Bottom":
                Activate(2);
                break;
            case "Left":
                Activate(3);
                break;
        }
    }

    void Activate(int index)
    {
        activations[index]++;

        var l = lights[index];
        l.color = Color.white;
        EffectManager.Instance.AddEffect(3, l.transform.position);

        DoLights(index);
        Move();
    }

    public void Deactivate(string dir)
    {
        switch (dir)
        {
            case "Top":
                Deactivate(0);
                break;
            case "Right":
                Deactivate(1);
                break;
            case "Bottom":
                Deactivate(2);
                break;
            case "Left":
                Deactivate(3);
                break;
        }
    }

    void Deactivate(int index)
    {
        activations[index]--;

        DoLights(index);
        Move();
    }

    void DoLights(int index)
    {
        var l = lights[index];
        l.color = activations[index] > 0 ? Color.white : new Color(0.25f, 0.25f, 0.25f);
    }
}
