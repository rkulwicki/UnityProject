using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{

    //turn managers on and off
    public bool battleManagerActive;
    public bool hudsManagerActive;

    //prefabs
    public GameObject battleManagerPrefab;
    public GameObject hudsManagerPrefab;

    //actual objects
    private GameObject _battleManager;
    private GameObject _hudsManager;

    void Awake()
    {
        InstantiateAllManagers();
    }

    private void Update()
    {
        //battle Manager
        if (battleManagerActive)
        {
            _battleManager.SetActive(true);
        }
        else
        {
            _battleManager.SetActive(false);
        }

        //huds Manager
        if (hudsManagerActive)
        {
            _hudsManager.SetActive(true);
        }
        else
        {
            _hudsManager.SetActive(false);
        }
    }

    private void InstantiateAllManagers()
    {
        _battleManager = Instantiate(battleManagerPrefab);
        _hudsManager = Instantiate(hudsManagerPrefab);
    }
}
