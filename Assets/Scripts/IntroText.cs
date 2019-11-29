using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    public TMPro.TextMeshPro textArea;

    private string message;
    private int messagePos;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        message = textArea.text;
        textArea.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (Random.value < 0.2f)
        {
            return;
        }

        if (messagePos >= 0 && !done)
        {
            messagePos++;

            if (messagePos > message.Length) return;

            string msg = message.Substring(0, messagePos);

            int openCount = msg.Split('(').Length - 1;
            int closeCount = msg.Split(')').Length - 1;

            //if (openCount > closeCount && useColors)
            //{
            //    msg += ")";
            //}

            string letter = message.Substring(messagePos - 1, 1);

            textArea.text = msg;

            if (messagePos == 1 || letter == " ")
            {
                //AudioManager.Instance.PlayEffectAt(25, transform.position, 0.5f);
                //AudioManager.Instance.PlayEffectAt(1, transform.position, 0.75f);
                //AudioManager.Instance.PlayEffectAt(Random.Range(33, 43), transform.position, 7f);
                AudioManager.Instance.PlayEffectAt(13, transform.position, 1.089f);
                AudioManager.Instance.PlayEffectAt(28, transform.position, 0.875f);
                AudioManager.Instance.PlayEffectAt(33, transform.position, 0.726f);
            }

            if (messagePos >= message.Length)
            {
                messagePos = -1;
                done = true;
            }
        }
    }
}
