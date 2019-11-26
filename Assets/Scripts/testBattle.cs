using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBattle : MonoBehaviour
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

        }
    }

    public void SetBattleField(GameObject[] fighers, GameObject tilemapFloor, GameObject tilemapObstacles)
    {

    }


}
