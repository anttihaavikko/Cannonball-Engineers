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
	public Blinders blinders;

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.Instance.levelToActivate = levelNumber;
        blinders.Close();
        Invoke("ChangeLevel", blinders.GetDuration());
    }

    void ChangeLevel()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Tweener.Instance)
            Tweener.Instance.ScaleTo(transform, Vector3.one, 0.1f, 0f, TweenEasings.QuadraticEaseOut);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Tweener.Instance)
            Tweener.Instance.ScaleTo(transform, Vector3.one * 0.9f, 0.1f, 0f, TweenEasings.QuadraticEaseOut);
    }
}
