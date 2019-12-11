using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour
{
    public GameObject grid;
    public GameObject tilemapFloor;
    public GameObject tilemapObstacles;

    public GameObject[] playersInvolved; //todo: many -> GameObject[]
    public GameObject[] enemiesInvolved; //todo: many -> GameObject[]

    public BattleState state;

    void Start()
    {
        state = BattleState.INACTIVE;
    }

    private void Update()
    {
        if (state != BattleState.INACTIVE)
        {
            if (state == BattleState.START)
            {

            } else if (state == BattleState.PLAYERTURN)
            {

            }
            else if (state == BattleState.ENEMYTURN)
            {

            }
            else if (state == BattleState.WON)
            {

            }
            else if (state == BattleState.LOST)
            {

            }
        }
    }

    void SetupBattle()
    {
        //BattleSystem is given
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject;
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject;
    }

    public void SetBattleField(GameObject[] fighers, GameObject tilemapFloor, GameObject tilemapObstacles)
    {
        //todo
        //call testGetTile to build a boundary
        //anything else? Maybe set locations of player and enemy but probably not?
    }

}
