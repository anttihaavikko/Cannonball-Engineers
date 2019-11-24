using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform text;
    public Image bg;
    public Color normalColor, hoverColor;
    public string activatesScene;
    public UnityEvent action;
    private bool done;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (done) return;

        if (!string.IsNullOrEmpty(activatesScene))
            SceneChanger.Instance.ChangeScene(activatesScene);

        if (action != null) action.Invoke();

        SceneChanger.Instance.cursor.Normalize();

        done = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (done) return;

        if (Tweener.Instance)
        {
            Tweener.Instance.ScaleTo(transform, Vector3.one, 0.2f, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ScaleTo(text, Vector3.one * 0.9f, 0.3f, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ColorTo(bg, hoverColor, 0.2f, 0f, TweenEasings.BounceEaseOut);
        }

        SceneChanger.Instance.cursor.Grow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (done) return;

        if (Tweener.Instance)
        {
            Tweener.Instance.ScaleTo(transform, Vector3.one * 0.9f, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
            Tweener.Instance.ScaleTo(text, Vector3.one, 0.1f, 0f, TweenEasings.QuadraticEaseOut);
            Tweener.Instance.ColorTo(bg, normalColor, 0.1f, 0f, TweenEasings.BounceEaseOut);
        }

        SceneChanger.Instance.cursor.Normalize();
    }
}
