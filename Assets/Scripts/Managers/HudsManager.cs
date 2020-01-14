//Name: HudsManager
//Desc: Turns each of the huds on and instantiates them.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HudsManager : MonoBehaviour
{
    //TODO: find and instantiate all prefabs with the "hud" tag
    //public GameObject[] hudPrefabs;
    //public GameObject[] huds;

    //only thing to interact with vvvv
    public bool playerMiniStatsHudActive;
    public bool battleHudActive;
    public bool playerBattleActionHudActive;
    //todo: public bool battleActionsActive; etc...

    //prefabs
    public GameObject playerMiniStatsHudPrefab;
    public GameObject battleHudPrefab;
    public GameObject playerBattleActionHudPrefab;

    //actualy intantiations of the objects
    public GameObject playerMiniStatsHud;
    public GameObject battleHud;
    public GameObject playerBattleActionHud;

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

        //player battle action hud
        if (playerBattleActionHudActive)
        {
            playerBattleActionHud.SetActive(true);
        }
        else
        {
            playerBattleActionHud.SetActive(false);
        }

    }

    private void InstatiateAllHuds()
    {
        playerMiniStatsHud = Instantiate(playerMiniStatsHudPrefab);
        battleHud = Instantiate(battleHudPrefab);
        playerBattleActionHud = Instantiate(playerBattleActionHudPrefab);
    }

    private void SetAllHudsToInactive()
    {
        playerMiniStatsHud.SetActive(false);
        battleHud.SetActive(false);
        playerBattleActionHud.SetActive(false);
    }
}
