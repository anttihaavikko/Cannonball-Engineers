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
    public GameObject locker;

	private bool done;
    private bool locked = true;

    public void Unlock()
    {
        locker.SetActive(false);
        locked = false;
    }

	public void OnPointerClick(PointerEventData eventData)
    {
		if (done || locked) return;

		Manager.Instance.levelToActivate = levelNumber;
        SceneChanger.Instance.ChangeScene("Main");

		SceneChanger.Instance.cursor.Normalize();

        AudioManager.Instance.PlayEffectAt(2, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(3, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(5, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(0, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 1f);

        done = true;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (done || locked) return;

		if (Tweener.Instance)
		{
			Tweener.Instance.ScaleTo(transform, Vector3.one, 0.1f, 0f, TweenEasings.BounceEaseOut);
			Tweener.Instance.ColorTo(bg, hoverColor, 0.2f, 0f, TweenEasings.BounceEaseOut);
		}

        AudioManager.Instance.PlayEffectAt(3, transform.position, 0.495f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 1.229f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 0.271f);

        SceneChanger.Instance.cursor.Grow();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		if (done || locked) return;

		if (Tweener.Instance)
		{
			Tweener.Instance.ScaleTo(transform, Vector3.one * 0.9f, 0.1f, 0f, TweenEasings.BounceEaseOut);
			Tweener.Instance.ColorTo(bg, normalColor, 0.1f, 0f, TweenEasings.BounceEaseOut);
		}

        AudioManager.Instance.PlayEffectAt(3, transform.position, 0.495f);
        AudioManager.Instance.PlayEffectAt(9, transform.position, 1.229f);
        AudioManager.Instance.PlayEffectAt(17, transform.position, 0.502f);

        SceneChanger.Instance.cursor.Normalize();
    }
}
