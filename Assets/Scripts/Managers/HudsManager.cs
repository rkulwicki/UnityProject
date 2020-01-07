//Name: HudsManager
//Desc: Turns each of the huds on and instantiates them.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudsManager : MonoBehaviour
{
    //only thing to interact with vvvv
    public bool playerMiniStatsHudActive;
    public bool battleHudActive;
    //todo: public bool battleActionsActive; etc...

    //prefabs
    public GameObject playerMiniStatsHudPrefab;
    public GameObject battleHudPrefab;

    //actualy intantiations of the objects
    public GameObject playerMiniStatsHud;
    public GameObject battleHud;

    private void Awake()
    {
        //instantiate new huds
        //destroy any existing huds
        var huds = GameObject.FindGameObjectsWithTag("Hud");
        if (huds.Length > 0) 
        {
            foreach(var hud in huds)
            {
                Destroy(hud);
            }
        }
        InstatiateAllHuds();
        SetAllHudsToInactive();
    }

    private void Update()
    {
        //player mini hud
        if (playerMiniStatsHudActive){
            playerMiniStatsHud.SetActive(true);
        } 
        else{
            playerMiniStatsHud.SetActive(false);
        }

        //battle hud
        if (battleHudActive){
            battleHud.SetActive(true);
        } 
        else{
            battleHud.SetActive(false);
        }

    }

    private void InstatiateAllHuds()
    {
        playerMiniStatsHud = Instantiate(playerMiniStatsHudPrefab);
        battleHud = Instantiate(battleHudPrefab);
    }

    private void SetAllHudsToInactive()
    {
        playerMiniStatsHud.SetActive(false);
        battleHud.SetActive(false);
    }
}
