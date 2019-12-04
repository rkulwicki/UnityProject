using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum turnEnum
    {
        player,
        enemy
    }
    public GameObject grid;
    public GameObject tilemapFloor;
    public GameObject tilemapObstacles;
    public turnEnum turn;
    public bool isBattle;

    // Start is called before the first frame update
    void Start()
    {
        
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject;
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattle)
        {
            //DO A LOT OF SHIT
            //todo
            //1. get player(s), get enemies
            //2. Set battle field.
            //3. take turns fighting each other like animals
        }
    }

    public void SetBattleField(GameObject[] fighers, GameObject tilemapFloor, GameObject tilemapObstacles)
    {
        //todo
        //call testGetTile to build a boundary
        //anything else? Maybe set locations of player and enemy but probably not?
    }


}
