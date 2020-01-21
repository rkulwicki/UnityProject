using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState {INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour
{

    public GameObject selectorPrefab;

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
        _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true; //actions hud on
        var playerBattleActionHud =_hudsManager.GetComponent<HudsManager>().playerBattleActionHud.GetComponent<PlayerBattleActionHudScript>();



        //=====================================
        //test!!!!
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Begin testing.");
            //LOG: You cannot create a monobehaviour using the 'new' keyword. Monobehaviour can only
            //  be added using AddComponent(). 
            var choose = new ChooseObject(selectorPrefab, enemiesInvolved, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3);
            //choose.InvokeChoose();
        }
        //=====================================

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

    public GameObject FindGameObjectWithTagFromArray(GameObject[] gameObjects, string tag)
    {
        var target = new GameObject();
        foreach(var gameObject in gameObjects)
        {
            if (gameObject.CompareTag(tag))
            {
                target = gameObject;
            }
        }
        if(target.name == "name") //default game object name
        {
            throw new MissingReferenceException("Game Object with tag " + tag + " not found.");
        }
        return target;
    }

    #endregion

    #region Button Click Helper Functions

    //TODO: this logic will be moved to PlayerBattleActionHudScript
    void TESTAttackButtonLogic()//BattleState battleState)
    {
        //TODO: WHO are we attacking?
        var battleActions = new BattleActions();
        //battleActions.Attack();
        Debug.Log(FindGameObjectWithTagFromArray(playersInvolved, "Player").GetComponent<PlayerStats>().baseDamage);
        state = BattleState.ENEMYTURN;
        _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
    }

    void TESTMoveButtonLogic()//BattleState battleState)
    {
        //TODO: WHO are we attacking?
        var battleActions = new BattleActions();
        //battleActions.Move();
        state = BattleState.ENEMYTURN;
        _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
    }

    #endregion
}
