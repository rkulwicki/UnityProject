using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState {INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour
{
    public GameObject grid;
    public GameObject tilemapFloor;
    public GameObject tilemapObstacles;

    public GameObject[] playersInvolved; 
    public GameObject[] enemiesInvolved; 

    public BattleState state;

    private GameObject _hudsManager;
    private GameObject _tileManager;

    void Start()
    {
        state = BattleState.INACTIVE;
        _hudsManager = GameObject.FindGameObjectWithTag("HudsManager");
        _tileManager = GameObject.FindGameObjectWithTag("TileManager");

    }

    private void Update()
    {
        if (state != BattleState.INACTIVE)
        {
            var battleBoundaryTilesLocations = BeforeStart();

            if (state == BattleState.START)
            {
                StartBattle();

            } else if (state == BattleState.PLAYERTURN)
            {
                PlayerTurn();
            }
            else if (state == BattleState.ENEMYTURN)
            {
                EnemyTurn();
            }
            else if (state == BattleState.WON)
            {
                Won(battleBoundaryTilesLocations);
            }
            else if (state == BattleState.LOST)
            {
                Lost(battleBoundaryTilesLocations);
            }
        }
    }

    #region Battle States

    private Vector3Int[] BeforeStart()
    {
        var battleBoundaryTilesLocationsList = new List<Vector3Int>();

        var initiatedEnemy = enemiesInvolved[0]; //todo: this will need to be handled

        Vector2Int center = new Vector2Int((int)initiatedEnemy.transform.position.x, (int)initiatedEnemy.transform.position.y);
        int size = initiatedEnemy.GetComponent<EnemyStats>().enemyBattleRadius;
        Tile tile = initiatedEnemy.GetComponent<EnemyStats>().battleBoundaryTile;

        var battleBoundaryTilesLocations = _tileManager.GetComponent<TileManager>().GenerateSquareTilesWithCenter(center, size, new Tile[1] { tile });
        return battleBoundaryTilesLocations;
    }

    private void StartBattle()
    {
        _hudsManager.GetComponent<HudsManager>().playerMiniStatsHudActive = true; //battle hud on
        DecideWhoGoesFirst();

    }

    private void PlayerTurn()
    {
        //pull up a new hud for choosing an action. See hud manager
        //for now, let's just make the back-end commands

        //todo: wait until player chooses button
        _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true; //actions hud on
        //_hudsManager.GetComponent<HudsManager>().playerBattleActionHud. [GET BUTTONS]

    }

    private void EnemyTurn()
    {

    }

    private void Won(Vector3Int[] battleBoundaryTilesLocations)
    {
        BattleEnd(battleBoundaryTilesLocations);
    }

    private void Lost(Vector3Int[] battleBoundaryTilesLocations)
    {
        BattleEnd(battleBoundaryTilesLocations);
    }

    #endregion

    #region Helper Functions

    void SetupBattle()
    {
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject;
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject;
    }

    private void DecideWhoGoesFirst()
    {
        //todo
        state = BattleState.PLAYERTURN; //default player goes first
    }

    public void SetBattleArea()
    {
        //todo
    }

    public void BattleEnd(Vector3Int[] boundaryTileLocations)
    {
        _hudsManager.GetComponent<HudsManager>().playerMiniStatsHudActive = false;
        _hudsManager.GetComponent<HudsManager>().battleHudActive = false;

        _tileManager.GetComponent<TileManager>().RemoveTiles(boundaryTileLocations); //delete boundary tiles
    }

    public void FreezeRigidBodies(GameObject[] gameObjects)
    {
        foreach(var gameObject in gameObjects)
        {
            if (gameObject.GetComponent<Rigidbody2D>() != null)
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnfreezeRigidBodies(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.GetComponent<Rigidbody2D>() != null)
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    #endregion

}
