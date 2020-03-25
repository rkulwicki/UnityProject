using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxScript: MonoBehaviour
{
    public GameObject textBoxObject;

    public string text;

    public GameObject textBoxPrefab; //using the textbox prefab
    public GameObject textTalkIconPrefab;

    public int lineInText;

    public bool stillTyping; //this comes from UITextTypeWriter. Probably

    public Vector3 pointOfSpeaking;

    private string inputKey = "space";
    private GameObject _playerObject;
    private TextReader _textReader;

    private TextBoxGlobal _textBoxGlobal;
    private DPadGlobal _dPadGlobal;

    private HudsManager _hudsManager;

    private GameObject _textIconObj;

    private bool _isListenForAPress = false;
    private bool _startTextBox = false;

    private void Start()
    {
        _textBoxGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<TextBoxGlobal>();
        _dPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
        _hudsManager = GameObject.FindGameObjectWithTag("HudsManager").GetComponent<HudsManager>();
        _playerObject = GameObject.Find("Player");
        _textReader = gameObject.GetComponent<TextReader>();

        //create icon
        _textIconObj = Instantiate(textTalkIconPrefab);
        _textIconObj.transform.parent = gameObject.transform.parent.transform;
        _textIconObj.transform.position = gameObject.transform.parent.transform.position + pointOfSpeaking;
        _textIconObj.SetActive(false);

        if (_textReader.lines.Length > 0)
        {
            text = _textReader.lines[lineInText];
        }
    }

    private void Update()
    {
        ListenForAPress();

        if (_startTextBox)
        {
            _textIconObj.SetActive(false);
            _hudsManager.dPadHudActive = false;
            _dPadGlobal.AllButtonsFalse();
            _playerObject.GetComponent<PlayerMove>().canMove = false;

            if (_textBoxGlobal.textBoxButton)
            {
                _textBoxGlobal.textBoxButton = false;  //turn dpad off

                //check if we are still typing. If we can just 
                if (textBoxObject.transform.Find("TextBox").Find("Text").GetComponent<UITextTypeWriter>().isTyping)
                {
                    textBoxObject.transform.Find("TextBox").Find("Text").GetComponent<UITextTypeWriter>().isStopTyping = true;
                }
                else
                {
                    if (lineInText < _textReader.lines.Length - 1)
                    {
                        lineInText++;
                    }
                    else if (lineInText == _textReader.lines.Length - 1)
                    {
                        DestroyTextBox();
                        _startTextBox = false;
                        _playerObject.GetComponent<PlayerMove>().canMove = true;
                        lineInText = 0; //this is where we start over.
                    }
                    text = _textReader.lines[lineInText];
                    textBoxObject.GetComponent<TextHolder>().text = text; //text changed
                    textBoxObject.GetComponent<TextHolder>().isTextDifferent = true; //alert that text changed.
                }
            }
        }
        else
        {
            _hudsManager.dPadHudActive = true; //turn dpad on
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _isListenForAPress = true;
            _textIconObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _isListenForAPress = false;
            _textIconObj.SetActive(false);
        }
    }

    private void ListenForAPress()
    {
        if (_isListenForAPress && _dPadGlobal.AButton)
        {
            text = _textReader.lines[lineInText];
            SpawnTextBox(textBoxPrefab);
            _startTextBox = true;
        }
    }

    #region CreateAndDestroy - SpawnTextBox, DestroyTextBox

    public void SpawnTextBox(GameObject textBoxPrefab)
    {
        textBoxObject = Instantiate(textBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        textBoxObject.GetComponent<TextHolder>().text = text;
        //insert "text" into text box dialogue
    }

    public void DestroyTextBox()
    {
        Destroy(textBoxObject);
    }

    #endregion
}
