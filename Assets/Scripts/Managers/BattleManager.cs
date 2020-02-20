using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState {INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour, IManager
{

    public GameObject selectorPrefab;

    public GameObject grid;
    public GameObject tilemapFloor;
    public GameObject tilemapObstacles;

    public GameObject[] playersInvolved; 
    public GameObject[] enemiesInvolved;

    public GameObject currentEnemy;

    public BattleState state;

    private GameObject _hudsManager;
    private GameObject _tileManager;
    private ChooseObjectWithBools _chooseObjectWithBools;
    private PlayerBattleGlobal _playerBattleGlobal;
    private DPadGlobal _dPadGlobal;
    private GameObject _player;

    private bool setUpState, takeDownState;

    void Start()
    {
        state = BattleState.INACTIVE;
        _hudsManager = GameObject.FindGameObjectWithTag("HudsManager");
        _tileManager = GameObject.FindGameObjectWithTag("TileManager");
        //_chooseObjectWithKeys = gameObject.GetComponent<ChooseObjectWithKeys>();
        _chooseObjectWithBools = gameObject.GetComponent<ChooseObjectWithBools>();
        _playerBattleGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<PlayerBattleGlobal>();
        _dPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
        setUpState = true;
        DecideWhoGoesFirst();
    }

    private void PlayerTurn()
    {
        if (setUpState)
        {
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true;
            var playerBattleButtons = _hudsManager.GetComponent<HudsManager>().playerBattleActionHud.GetComponent<PlayerBattleButtons>();
            setUpState = false;
        }
        
        
        //                      ATTACK
        // ==================================================
        if (_playerBattleGlobal.AttackButton)
        {
            //TODO: first choose type of attack (goes here before StartChoose)
            _playerBattleGlobal.AttackButton = false;
            _chooseObjectWithBools.StartChoose(selectorPrefab, enemiesInvolved);
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
        }

        if (_chooseObjectWithBools.result != null) //after chosen
        {
            currentEnemy = _chooseObjectWithBools.currentObject;
            //TODO:
            //Here. Insert logic for attacking, given the attack and the chosen enemy.
            //^^ currently just the enemy. Attack choice will be later.
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
            var baseD = _player.GetComponent<PlayerStats>().baseDamage;
            var curEnStats = currentEnemy.GetComponent<EnemyStats>();
            var playerActions = _player.GetComponent<PlayerBattleActions>();
            playerActions.Attack(baseD, curEnStats);
            _chooseObjectWithBools.result = null; //reset the choice.
            takeDownState = true;
        }
        // ==================================================

        if (takeDownState)
        {
            //do ending stuff, probably switch to enemy turn.
            Debug.Log("End player turn.");
        }
    }

    private void EnemyTurn()
    {
        //TODO
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
