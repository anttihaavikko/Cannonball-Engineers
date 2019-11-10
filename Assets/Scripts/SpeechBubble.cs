using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	public TextMeshPro textArea;
    public SpriteRenderer helpImage;
    public Sprite helpSprite;
    public Vector3 hiddenSize;

    private bool shown;
	private string message = "";
	private int messagePos = -1;
    private bool hidesWithAny = false;
    private Vector3 shownSize;

    public bool done = false;

	private AudioSource audioSource;

	private List<string> messageQue;

	public Color hiliteColor;
    string hiliteColorHex;

    bool useColors = true;
    private bool canSkip = false;

    private string[] options;
    private string[] optionActions;
    private int optionSelection;

    private Vector3 helpImageSize;


    // Use this for initialization
    void Start () {
		audioSource = GetComponent<AudioSource> ();

		messageQue = new List<string> ();

        shownSize = transform.localScale;
        transform.localScale = hiddenSize;

        Invoke("EnableSkip", 0.25f);

        if (helpSprite)
            helpImage.sprite = helpSprite;

        helpImageSize = helpImage.transform.localScale;
        helpImage.transform.localScale = Vector3.zero;
    }

    void EnableSkip()
    {
        canSkip = true;
    }

    // Update is called once per frame
    void Update () {

		if (Random.value < 0.2f) {
			return;
		}

		if (messagePos >= 0 && !done) {
			messagePos++;

            if (messagePos > message.Length) return;

			string msg = message.Substring (0, messagePos);

			int openCount = msg.Split('(').Length - 1;
			int closeCount = msg.Split(')').Length - 1;

            if (openCount > closeCount && useColors) {
				msg += ")";
			}

			string letter = message.Substring (messagePos - 1, 1);

            if(letter == "#")
            {
                done = true;
                textArea.text += " ";
                Tweener.Instance.ScaleTo(helpImage.transform, helpImageSize, 0.3f, 0f, TweenEasings.BounceEaseOut);
                return;
            }

            textArea.text = useColors ? msg.Replace("(", "<color=" + hiliteColorHex + ">").Replace(")", "</color>") : msg;

            if (messagePos == 1 || letter == " ") {
				//AudioManager.Instance.PlayEffectAt(25, transform.position, 0.5f);
				//AudioManager.Instance.PlayEffectAt(1, transform.position, 0.75f);
				//AudioManager.Instance.PlayEffectAt(Random.Range(33, 43), transform.position, 7f);
			}

			if (messagePos >= message.Length) {
				messagePos = -1;

				done = true;
			}
		}
	}

	public int QueCount() {
		return messageQue.Count;
	}

	public void SkipMessage() {
		done = true;
		messagePos = -1;
		textArea.text = message;
	}

    public void ShowMessage(string str, bool colors = true) {
        hidesWithAny = false;
        if(helpImage) helpImage.transform.localScale = Vector3.zero;
        canSkip = false;
        Invoke("EnableSkip", 0.25f);

        Tweener.Instance.ScaleTo(transform, shownSize, 0.6f, 0f, TweenEasings.BounceEaseOut);

        //AudioManager.Instance.PlayEffectAt(9, transform.position, 1f);
        //AudioManager.Instance.PlayEffectAt(27, transform.position, 0.7f);

        useColors = colors;

        //AudioManager.Instance.Highpass ();

		done = false;
		shown = true;
		message = str;
		textArea.text = "";

		Invoke ("ShowText", 0.2f);
    }

	public void QueMessage(string str) {
		messageQue.Add (str);
	}

	public void CheckQueuedMessages() {
		if (messageQue.Count > 0 && !shown) {
			PopMessage ();
		}
	}

	private void PopMessage() {
		string msg = messageQue [0];
		messageQue.RemoveAt (0);
		ShowMessage (msg);
	}

	private void ShowText() {
		messagePos = 0;
	}

	public void HideAfter (float delay) {
		Invoke ("Hide", delay);
	}

	public void Hide() {

        Tweener.Instance.ScaleTo(transform, hiddenSize, 0.3f, 0f, TweenEasings.QuadraticEaseOut);

        //AudioManager.Instance.Highpass (false);

        //AudioManager.Instance.PlayEffectAt (9, transform.position, 1f);
        //AudioManager.Instance.PlayEffectAt(27, transform.position, 0.7f);

        shown = false;
		textArea.text = "";
	}

	public bool IsShown() {
		return shown;
	}

	public void SetColor(Color color) {
        hiliteColorHex = "#" + ColorUtility.ToHtmlStringRGB (color);
	}
}
