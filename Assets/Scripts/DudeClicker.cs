using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeClicker : MonoBehaviour
{
    public Transform follow;
    public Dude dude;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(follow.position.x, follow.position.y, -0.1f);
    }

    private void OnMouseEnter()
    {
        if (!dude.CanBeActivated() || dude == Manager.Instance.activeDude) return;

        Manager.Instance.hoveredDude = dude;
        dude.ToggleOutline(true);

        SceneChanger.Instance.cursor.Shrink();
    }

    private void OnMouseExit()
    {
        dude.ToggleOutline(false);
        Manager.Instance.hoveredDude = null;

        SceneChanger.Instance.cursor.Normalize();
    }

    private void OnMouseUp()
    {
        if (!dude.CanBeActivated()) return;

        Manager.Instance.ChangeDude(dude);
        dude.ToggleOutline(false);
        EffectManager.Instance.AddEffect(5, dude.body.transform.position);
        EffectManager.Instance.AddEffect(7, dude.body.transform.position);

        AudioManager.Instance.PlayEffectAt(9, dude.body.transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, dude.body.transform.position, 0.244f);
        AudioManager.Instance.PlayEffectAt(18, dude.body.transform.position, 0.429f);
        AudioManager.Instance.PlayEffectAt(21, dude.body.transform.position, 1.607f);
        AudioManager.Instance.PlayEffectAt(25, dude.body.transform.position, 1.321f);


        SceneChanger.Instance.cursor.Normalize();

        if (dude.launcher)
        {
            dude.launcher.ActivateIfReserve(dude);
        }
    }
}
