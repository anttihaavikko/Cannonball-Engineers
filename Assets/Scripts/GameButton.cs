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

        AudioManager.Instance.Lowpass(false);
        AudioManager.Instance.Highpass(false);

        AudioManager.Instance.PlayEffectAt(2, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(3, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(5, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(0, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 1f);
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

        AudioManager.Instance.PlayEffectAt(3, transform.position, 0.495f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 1.229f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 0.271f);

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

        AudioManager.Instance.PlayEffectAt(3, transform.position, 0.495f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 1.229f);
        AudioManager.Instance.PlayEffectAt(17, transform.position, 0.502f);

        SceneChanger.Instance.cursor.Normalize();
    }
}
