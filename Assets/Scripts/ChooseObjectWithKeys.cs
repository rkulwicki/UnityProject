using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ChooseObjectWithKeys : MonoBehaviour
{

    public GameObject currentObject;
    public int current;
    public int max;

    public GameObject result;

    public bool choosing = false;

    private GameObject selectorPrefab;
    private GameObject selector;
    private GameObject[] gameObjects;
    private KeyCode keycodeChoose;
    private KeyCode keycodeForwardInList;
    private KeyCode keycodeBackwardInList;


    /// <summary>
    /// Given a list of game objects, generate a GUI to choose from the list. Starts coroutine instantly after instantiation.
    /// </summary>
    public void StartChoose(GameObject selectorPrefab,
                        GameObject[] gameObjects,
                        KeyCode keycodeChoose,
                        KeyCode keycodeForwardInList,
                        KeyCode keycodeBackwardInList)
    {
        this.selectorPrefab = selectorPrefab;
        this.keycodeChoose = keycodeChoose;
        this.keycodeForwardInList = keycodeForwardInList;
        this.keycodeBackwardInList = keycodeBackwardInList;
        this.gameObjects = gameObjects;
        //this.keycodeChoose = globalInputs.TestMessage();
        current = 0;
        max = gameObjects.Length - 1;
        currentObject = gameObjects[current];
        selector = GenerateUISelector(selectorPrefab, gameObjects[current].transform.position); //generates UI at enemy location
        choosing = true;
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
            StartCoroutine(MoveThroughList(keycodeForwardInList, keycodeBackwardInList, gameObjects));
            StartCoroutine(WaitForKeyDown(keycodeChoose));
        }
    }

    private IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
        {
            yield return null;
        }
        //Executes when you choose
        choosing = false;
        result = currentObject;
        Destroy(selector);
        StopAllCoroutines();
    }

    private IEnumerator MoveThroughList(KeyCode forwards, KeyCode backwards, GameObject[] gameObjects)
    {
        if (Input.GetKeyDown(forwards))
        {
            current++;
            if (current > max) //reached end of list
            {
                current = 0;
            }
            currentObject = gameObjects[current];
            selector.transform.position = currentObject.transform.position;
        }
        else if (Input.GetKeyDown(backwards))
        {
            current--;
            if (current < 0)
            {
                current = max;
            }
            currentObject = gameObjects[current];
            selector.transform.position = currentObject.transform.position;
        }
        yield return null;
    }
}

