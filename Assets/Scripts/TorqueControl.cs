using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorqueControl : MonoBehaviour
{
    public Launcher launcher;

    private void OnMouseEnter()
    {
        SceneChanger.Instance.cursor.TorqueControl(true);
        Manager.Instance.isHoveringSomething = true;
    }

    private void OnMouseExit()
    {
        SceneChanger.Instance.cursor.TorqueControl(false);
        Manager.Instance.isHoveringSomething = false;
    }

    private void OnMouseDown()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var localPos = transform.InverseTransformPoint(pos);
        launcher.SetTorque(true, -localPos.x);
    }

    private void OnMouseUp()
    {
        AudioManager.Instance.PlayEffectAt(2, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(17, transform.position, 0.43f);
    }

    private void OnMouseDrag()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var localPos = transform.InverseTransformPoint(pos);
        launcher.SetTorque(true, -localPos.x);
    }
}
