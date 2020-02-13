using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPad : MonoBehaviour
{
    public Button buttonUp, buttonRight, buttonDown, buttonLeft;

    //[Injected]
    private GameObject _mainGame;
    void Start()
    {
        //buttonUp.onClick.AddListener(TaskOnClickUp);
        //buttonRight.onClick.AddListener(TaskOnClickRight);
        //buttonDown.onClick.AddListener(TaskOnClickDown);
        //buttonLeft.onClick.AddListener(TaskOnClickLeft);

        _mainGame = GameObject.FindGameObjectWithTag("MainGame");
    }

    public void DPadUpPressed()
    {
        Debug.Log("UP");
        _mainGame.GetComponent<DPadGlobal>().DPadUp = true;
    }
    public void DPadUpRelease()
    {
        Debug.Log("UP released");
        _mainGame.GetComponent<DPadGlobal>().DPadUp = false;
    }

    public void DPadRightPressed()
    {
        Debug.Log("RIGHT");
        _mainGame.GetComponent<DPadGlobal>().DPadRight = true;
    }
    public void DPadRightRelease()
    {
        Debug.Log("Right released");
        _mainGame.GetComponent<DPadGlobal>().DPadRight = false;
    }

    public void DPadLeftPressed()
    {
        Debug.Log("LEFT");
        _mainGame.GetComponent<DPadGlobal>().DPadLeft = true;
    }
    public void DPadLeftRelease()
    {
        Debug.Log("LEFT released");
        _mainGame.GetComponent<DPadGlobal>().DPadLeft = false;
    }

    public void DPadDownPressed()
    {
        Debug.Log("DOWN");
        _mainGame.GetComponent<DPadGlobal>().DPadDown = true;
    }
    public void DPadDownRelease()
    {
        Debug.Log("DOWN released");
        _mainGame.GetComponent<DPadGlobal>().DPadDown = false;
    }
}
