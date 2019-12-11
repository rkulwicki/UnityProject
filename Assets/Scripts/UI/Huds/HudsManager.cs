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

    //prefabs
    public GameObject playerMiniStatsHudPrefab;
    public GameObject battleHudPrefab;

    //actualy intantiations of the objects
    private GameObject playerMiniStatsHud;
    private GameObject battleHud;

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
        playerMiniStatsHud = Instantiate(playerMiniStatsHudPrefab);
        battleHud = Instantiate(battleHudPrefab);

        //set to inactive to start
        playerMiniStatsHud.SetActive(false);
        battleHud.SetActive(false);
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
}
