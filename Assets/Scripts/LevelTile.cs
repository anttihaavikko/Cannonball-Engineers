using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text num1, num2;
    public Text text;
    public int levelNumber;
    public Text time;
    public List<GameObject> stars;
	public Color normalColor, hoverColor;
	public Image bg;

	public void OnPointerClick(PointerEventData eventData)
    {
        Manager.Instance.levelToActivate = levelNumber;
        SceneChanger.Instance.ChangeScene("Main");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (Tweener.Instance)
		{
			Tweener.Instance.ScaleTo(transform, Vector3.one, 0.1f, 0f, TweenEasings.BounceEaseOut);
			Tweener.Instance.ColorTo(bg, hoverColor, 0.2f, 0f, TweenEasings.BounceEaseOut);
		}

        SceneChanger.Instance.cursor.Grow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		if (Tweener.Instance)
		{
			Tweener.Instance.ScaleTo(transform, Vector3.one * 0.9f, 0.1f, 0f, TweenEasings.BounceEaseOut);
			Tweener.Instance.ColorTo(bg, normalColor, 0.1f, 0f, TweenEasings.BounceEaseOut);
		}

        SceneChanger.Instance.cursor.Normalize();
    }
}
