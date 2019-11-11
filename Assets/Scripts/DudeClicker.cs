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
        if (dude.IsDone() || dude == Manager.Instance.activeDude) return;

        Manager.Instance.hoveredDude = dude;
        dude.ToggleOutline(true);
    }

    private void OnMouseExit()
    {
        dude.ToggleOutline(false);
        Manager.Instance.hoveredDude = null;
    }

    private void OnMouseUp()
    {
        if (dude.IsDone()) return;

        Manager.Instance.ChangeDude(dude);
        dude.ToggleOutline(false);
        EffectManager.Instance.AddEffect(5, dude.body.transform.position);

        if(dude.launcher)
        {
            dude.launcher.ActivateIfReserve(dude);
        }
    }
}
