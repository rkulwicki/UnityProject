using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ChooseObjectWithBools : MonoBehaviour
{
    public GameObject currentObject;
    public int current;
    public int max;

    public GameObject result;

    public bool choosing = false;

    private GameObject selectorPrefab;
    private GameObject selector;
    private GameObject[] gameObjects;

    private bool _forward;
    private bool _backward;
    private bool _choose;

    //NOTE: This code depends on GlobalInputs!!!
    private DPadGlobal _dPadGlobal;


    /// <summary>
    /// Given a list of game objects, generate a GUI to choose from the list. Starts coroutine instantly after instantiation.
    /// </summary>
    public void StartChoose(GameObject selectorPrefab, GameObject[] gameObjects)
    {
        this.selectorPrefab = selectorPrefab;
        this.gameObjects = gameObjects;
        //this.keycodeChoose = globalInputs.TestMessage();
        current = 0;
        max = gameObjects.Length - 1;
        currentObject = gameObjects[current];
        selector = GenerateUISelector(selectorPrefab, gameObjects[current].transform.position); //generates UI at enemy location
        choosing = true;
        _dPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
    }

    private GameObject GenerateUISelector(GameObject selectorPrefab, Vector3 position)
    {
        var selector = Instantiate(selectorPrefab, position, Quaternion.identity);
        return selector;
    }

    private void Update()
    {
        if (choosing)
        {
            if (_dPadGlobal.DPadUp || _dPadGlobal.DPadRight)
            {
                current++;
                if (current > max) //reached end of list
                {
                    current = 0;
                }
                currentObject = gameObjects[current];
                selector.transform.position = currentObject.transform.position;
                _dPadGlobal.DPadUp = false;
                _dPadGlobal.DPadRight = false;
            }
            else if (_dPadGlobal.DPadDown || _dPadGlobal.DPadLeft)
            {
                current--;
                if (current < 0)
                {
                    current = max;
                }
                currentObject = gameObjects[current];
                selector.transform.position = currentObject.transform.position;
                _dPadGlobal.DPadDown = false;
                _dPadGlobal.DPadLeft = false;
            }

            if (_dPadGlobal.AButton) //TODO: CHANGE TO CHOOSE BUTTON, NOT RIGHT BUTTON
            {
                choosing = false;
                result = currentObject;
                Destroy(selector);
            }
            
        }
    }
}


