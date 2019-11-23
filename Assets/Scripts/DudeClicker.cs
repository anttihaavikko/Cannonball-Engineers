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

        SceneChanger.Instance.cursor.Normalize();

        if (dude.launcher)
        {
            dude.launcher.ActivateIfReserve(dude);
        }
    }
}
