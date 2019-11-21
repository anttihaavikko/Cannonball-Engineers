using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform text;
    public Image bg;
    public Color normalColor, hoverColor;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Tweener.Instance)
        {
            Tweener.Instance.ScaleTo(transform, Vector3.one, 0.2f, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ScaleTo(text, Vector3.one * 0.9f, 0.3f, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ColorTo(bg, hoverColor, 0.2f, 0f, TweenEasings.BounceEaseOut);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Tweener.Instance)
        {
            Tweener.Instance.ScaleTo(transform, Vector3.one * 0.9f, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
            Tweener.Instance.ScaleTo(text, Vector3.one, 0.1f, 0f, TweenEasings.QuadraticEaseOut);
            Tweener.Instance.ColorTo(bg, normalColor, 0.1f, 0f, TweenEasings.BounceEaseOut);
        }
    }
}
