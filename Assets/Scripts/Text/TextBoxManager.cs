using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    public float textTime;

    public GameObject textBoxObject;

    public string text;

    public GameObject textBoxPrefab; //using the textbox prefab
    public int waitSeconds;

    public int lineInText;

    private string inputKey = "space";
    private GameObject _playerObject;
    private TextReader _textReader;

    private bool _inTextBox = false;

    private void Start()
    {
        _playerObject = GameObject.Find("Player");
        _textReader = gameObject.GetComponent<TextReader>();
        if(_textReader.lines.Length > 0)
        {
            //we will be doing some logic here to read line by line but for now
            //we will just read the first line
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
                if (lineInText < _textReader.lines.Length - 1)
                {
                    lineInText++;
                }
                else if (lineInText == _textReader.lines.Length - 1)
                {
                    DestroyTextBox();
                    _inTextBox = false;
                    _playerObject.GetComponent<PlayerMove>().canMove = true;
                }
                text = _textReader.lines[lineInText];
                textBoxObject.GetComponent<TextHolder>().text = text; //text changed
                textBoxObject.GetComponent<TextHolder>().isTextDifferent = true; //alert that text changed.
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

    #region UI

    #endregion

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
