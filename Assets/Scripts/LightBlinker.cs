using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightBlinker : MonoBehaviour
{
    public List<SpriteRenderer> lights;

    private SpriteRenderer[] activeLights;

    // Start is called before the first frame update
    void Start()
    {
        activeLights = new SpriteRenderer[2];
        ChangeLight();
    }

    void ChangeLight()
    {
        DoChange(0);
        DoChange(1);

        Invoke("ChangeLight", Random.Range(0.2f, 2f));
    }

    void DoChange(int index)
    {
        if (activeLights[index])
        {
            var c = new Color(0.25f, 0.25f, 0.25f);
            Tweener.Instance.ColorTo(activeLights[index], c, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
        }

        activeLights[index] = lights[Random.Range(0, lights.Count)];
        Tweener.Instance.ColorTo(activeLights[index], Color.white, 0.2f, 0f, TweenEasings.QuadraticEaseOut);

        //EffectManager.Instance.AddEffect(2, activeLights[index].transform.position);
    }
}
