//Name: HudsManager
//Desc: Turns each of the huds on and instantiates them.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HudsManager : MonoBehaviour, IManager
{
    //TODO: find and instantiate all prefabs with the "hud" tag
    //public GameObject[] hudPrefabs;
    //public GameObject[] huds;

    //only thing to interact with vvvv
    public bool playerMiniStatsHudActive;
    public bool battleHudActive;
    public bool playerBattleActionHudActive;
    public bool dPadHudActive;
    //todo: public bool battleActionsActive; etc...

    //prefabs
    public GameObject playerMiniStatsHudPrefab;
    public GameObject battleHudPrefab;
    public GameObject playerBattleActionHudPrefab;
    public GameObject dPadHudPrefab;

    //actualy intantiations of the objects
    public GameObject playerMiniStatsHud;
    public GameObject battleHud;
    public GameObject playerBattleActionHud;
    public GameObject dPadHud;

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

    //start may need to be altered.
    private void Start()
    {
        dPadHudActive = true;
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

        //dpad hud
        if (dPadHudActive)
        {
            dPadHud.SetActive(true);
        }
        else
        {
            dPadHud.SetActive(false);
        }

    }

    public void BattleActionsHudBack()
    {
        //get panels
        var panel1 = playerBattleActionHud.transform.Find("PlayerBattleButtons").gameObject;
        var panel2 = panel1.transform.Find("ActionsPanel").gameObject;
        var panel3 = panel2.transform.Find("AttackListPanel").gameObject;

        playerBattleActionHudActive = true;

        if(panel1.activeSelf == true && panel2.activeSelf == false && panel3.activeSelf == false)
        {
            //stay the same
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
        }
        else if (panel1.activeSelf == true && panel2.activeSelf == true && panel3.activeSelf == false)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
        }
        else if(panel1.activeSelf == true && panel2.activeSelf == true && panel3.activeSelf == true)
        {
            panel1.SetActive(true);
            panel2.SetActive(true);
            panel3.SetActive(false);
        }
        else //all panels collapsed
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
        }
    }

    public void CollapseBattleActionsHud()
    {
        //get panels
        var panel1 = playerBattleActionHud.transform.Find("PlayerBattleButtons").gameObject;
        var panel2 = panel1.transform.Find("ActionsPanel").gameObject;
        var panel3 = panel2.transform.Find("AttackListPanel").gameObject;
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    private void InstatiateAllHuds()
    {
        playerMiniStatsHud = Instantiate(playerMiniStatsHudPrefab);
        battleHud = Instantiate(battleHudPrefab);
        playerBattleActionHud = Instantiate(playerBattleActionHudPrefab);
        dPadHud = Instantiate(dPadHudPrefab);
    }

    private void SetAllHudsToInactive()
    {
        playerMiniStatsHud.SetActive(false);
        battleHud.SetActive(false);
        playerBattleActionHud.SetActive(false);
        dPadHud.SetActive(false);
    }
}
