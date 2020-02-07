using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerHandler : MonoBehaviour
{

    //turn managers on and off
    public bool battleManagerActive;
    public bool hudsManagerActive;
    public bool tileSetterManagerActive;

    //prefabs
    public GameObject battleManagerPrefab;
    public GameObject hudsManagerPrefab;
    public GameObject tileSetterManagerPrefab;

    //actual objects
    private GameObject _battleManager;
    private GameObject _hudsManager;
    private GameObject _tileSetterManager;

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

        //tile setter Manager
        if (tileSetterManagerActive)
        {
            _tileSetterManager.SetActive(true);
        }
        else
        {
            _tileSetterManager.SetActive(false);
        }
    }

    private void InstantiateAllManagers()
    {
        _battleManager = Instantiate(battleManagerPrefab);
        _hudsManager = Instantiate(hudsManagerPrefab);
        _tileSetterManager = Instantiate(tileSetterManagerPrefab);
    }
}
