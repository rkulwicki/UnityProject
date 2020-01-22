using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class ChooseObject : MonoBehaviour
{

    public GameObject currentObject;
    public int current;
    public int max;

    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    private GameObject selectorPrefab;
    private GameObject selector;
    private GameObject[] gameObjects;
    private KeyCode keycodeChoose;
    private KeyCode keycodeForwardInList;
    private KeyCode keycodeBackwardInList;

    public bool choosing = false;

    [Description("Given a list of game objects, generate a GUI to choose from the list. Starts" +
        " coroutine instantly after instantiation.")]
    public ChooseObject(GameObject selectorPrefab,
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
        current = 0;
        max = gameObjects.Length;
        currentObject = gameObjects[current];
        selector = GenerateUISelector(selectorPrefab, gameObjects[current].transform.position); //generates UI at enemy location
    }

    //public IEnumerator InvokeChoose() //HALP
    //{
    //    Debug.Log("WOAH! ChooseObject, Line 47");
    //    CoroutineWithData cd = new CoroutineWithData(this, Choose());
    //    yield return cd.coroutine;
    //    Debug.Log("result is " + cd.result);
    //}

    ////contains nested coroutines
    //public IEnumerator Choose()
    //{
    //    while (!isChosen)
    //    {
    //        StartCoroutine(MoveThroughList(keycodeForwardInList, keycodeBackwardInList, gameObjects));
    //        StartCoroutine(WaitForKeyDown(keycodeChoose));
    //    }
    //    yield return currentObject;
    //}

    public void StartChoose()
    {
        choosing = true;
    }

    private void Update()
    {
        while (!choosing)
        {
            StartCoroutine(MoveThroughList(keycodeForwardInList, keycodeBackwardInList, gameObjects));
            StartCoroutine(WaitForKeyDown(keycodeChoose));
        }
        
    }

    private GameObject GenerateUISelector(GameObject selectorPrefab, Vector3 position)
    {
        var selector = Instantiate(selectorPrefab, position, Quaternion.identity);
        return selector;
    }

    private IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
        {
            yield return null;
        }
        choosing = false;
    }

    private IEnumerator MoveThroughList(KeyCode forwards, KeyCode backwards, GameObject[] gameObjects)
    {
        while (true)
        {
            if (Input.GetKeyDown(forwards))
            {
                current++;
                if (current > max) //reached end of list
                {
                    current = 0;
                }
                currentObject = gameObjects[current];
            }
            else if (Input.GetKeyDown(backwards))
            {
                current--;
                if (current < 0)
                {
                    current = max;
                }
                currentObject = gameObjects[current];
            }
            yield return null;
        }
    }
}

