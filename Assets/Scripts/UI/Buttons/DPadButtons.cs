﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPadButtons : MonoBehaviour
{
    public Button buttonUp, buttonRight, buttonDown, buttonLeft;

    //example: buttonUp.onClick.AddListener(TaskOnClickUp);
    //[Injected]
    private GameObject _globalInputs;
    void Start()
    {   
        _globalInputs = GameObject.FindGameObjectWithTag("GlobalInputs");
    }

    public void DPadUpPressed()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadUp = true;
    }
    public void DPadUpRelease()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadUp = false;
    }

    public void DPadRightPressed()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadRight = true;
    }
    public void DPadRightRelease()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadRight = false;
    }

    public void DPadLeftPressed()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadLeft = true;
    }
    public void DPadLeftRelease()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadLeft = false;
    }

    public void DPadDownPressed()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadDown = true;
    }
    public void DPadDownRelease()
    {
        _globalInputs.GetComponent<DPadGlobal>().DPadDown = false;
    }
}
