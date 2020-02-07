using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPadHud : MonoBehaviour
{
    public Button buttonUp, buttonRight, buttonDown, buttonLeft;

    void Start()
    {
        buttonUp.onClick.AddListener(TaskOnClickUp);
        buttonRight.onClick.AddListener(TaskOnClickRight);
        buttonDown.onClick.AddListener(TaskOnClickDown);
        buttonLeft.onClick.AddListener(TaskOnClickLeft);
    }

    void TaskOnClickUp()
    {
        Debug.Log("UP");
    }

    void TaskOnClickRight()
    {
        Debug.Log("RIGHT");
    }

    void TaskOnClickDown()
    {
        Debug.Log("DOWN");
    }

    void TaskOnClickLeft()
    {
        Debug.Log("LEFT");
    }
}
