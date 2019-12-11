using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;

public class UITextTypeWriter : MonoBehaviour
{
    public Text text;
    public bool playOnAwake = true;
    public float delayToStart;
    public float delayBetweenChars = 0.125f;
    public float delayAfterPunctuation = 0.5f;
    public bool isTyping;
    public bool isStopTyping;

    private string story;
    private float originDelayBetweenChars;
    private bool lastCharPunctuation = false;
    private char charComma;
    private char charPeriod;
    Coroutine lastRoutine = null;
    void Awake()
    {
        text = GetComponent<Text>();
        originDelayBetweenChars = delayBetweenChars;

        charComma = Convert.ToChar(44);
        charPeriod = Convert.ToChar(46);

        isStopTyping = false;

        if (playOnAwake)
        {
            ChangeText(text.text, delayToStart);
        }
    }

    //Update text and start typewriter effect
    public void ChangeText(string textContent, float delayBetweenChars = 0f)
    {
        isTyping = true;
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine); //stop Coroutime if exist
        }
        story = textContent;
        text.text = ""; //clean text
        Invoke("Start_PlayText", delayBetweenChars); //Invoke effect
    }

    void Start_PlayText()
    {
        lastRoutine = StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {

        foreach (char c in story)
        {
            delayBetweenChars = originDelayBetweenChars;

            if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
            {
                if (!isStopTyping)
                {
                    yield return new WaitForSeconds(delayBetweenChars = delayAfterPunctuation);
                }
                lastCharPunctuation = false;
            }

            if (c == charComma || c == charPeriod)
            {
                lastCharPunctuation = true;
            }

            text.text += c;
            if (!isStopTyping)
            {
                yield return new WaitForSeconds(delayBetweenChars);
            }
        }

        isTyping = false; //this could be implemented better. Because WaitForSeconds above
                          // will still wait even if it is at the end of the text but whateves.
        isStopTyping = false;
    }
}