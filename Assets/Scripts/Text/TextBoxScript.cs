using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script gets put onto a trigger and I guess really isn't a manager but
// shhhh don't tell anybody!

public class TextBoxScript: MonoBehaviour
{
    public GameObject textBoxObject;

    public string text;

    public GameObject textBoxPrefab; //using the textbox prefab

    public int lineInText;

    public bool stillTyping; //this comes from UITextTypeWriter. Probably

    private string inputKey = "space";
    private GameObject _playerObject;
    private TextReader _textReader;

    private bool _inTextBox = false;

    private void Start()
    {
        _playerObject = GameObject.Find("Player");
        _textReader = gameObject.GetComponent<TextReader>();
        if (_textReader.lines.Length > 0)
        {
            text = _textReader.lines[lineInText];
        }
    }

    private void Update()
    {

        if (_inTextBox)
        {
            _playerObject.GetComponent<PlayerMove>().canMove = false;

            if (Input.GetKeyUp(inputKey))
            {

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
                        _inTextBox = false;
                        _playerObject.GetComponent<PlayerMove>().canMove = true;
                        lineInText = 0; //this is where we start over.
                    }
                    text = _textReader.lines[lineInText];
                    textBoxObject.GetComponent<TextHolder>().text = text; //text changed
                    textBoxObject.GetComponent<TextHolder>().isTextDifferent = true; //alert that text changed.
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _inTextBox = true;
            text = _textReader.lines[lineInText];
            SpawnTextBox(textBoxPrefab);
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
