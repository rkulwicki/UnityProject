using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState { INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleManager : MonoBehaviour, IManager
{
    public int turnNumber;

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

    private bool _setUpState, _takeDownState, _isWon, _isLose;

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
    //update is the high level battle state. We break the states down even further down in the code.
    private void Update()
    {
        if (state != BattleState.INACTIVE)
        {
            var battleBoundaryTilesLocations = BeforeStart();

            if (state == BattleState.START)
            {
                StartBattle();
            }
            else if (state == BattleState.PLAYERTURN)
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

    //=====================================
    #region Battle States

    private Vector3Int[] BeforeStart()
    {
        turnNumber = 0;

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
        _setUpState = true;
        DecideWhoGoesFirst();
    }

    private void PlayerTurn()
    {
        if (_setUpState)
        {
            turnNumber = turnNumber + 1;
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true;
            var playerBattleButtons = _hudsManager.GetComponent<HudsManager>().playerBattleActionHud.GetComponent<PlayerBattleButtons>();
            _setUpState = false;
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
            //^^ currently just the enemy and attack is base damage. Attack choice will be later.
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
            var baseD = _player.GetComponent<PlayerStats>().baseDamage;
            var curEnStats = currentEnemy.GetComponent<EnemyStats>();
            var playerActions = _player.GetComponent<PlayerBattleActions>();
            playerActions.Attack(baseD, curEnStats);
            _chooseObjectWithBools.result = null; //reset the choice.

            _takeDownState = true;
        }
        // ==================================================

        if (_playerBattleGlobal.MoveButton)
        {
            //move
        }







        if (_takeDownState)
        {
            if (_isWon)
                state = BattleState.WON;
            if (_isLose)
                state = BattleState.LOST;
            //if not battle over vvvvvvv
            state = BattleState.ENEMYTURN;
            _takeDownState = false;
            _setUpState = true;
        }
    }

    private void EnemyTurn()
    {
        if (_setUpState)
        {
            //do stuff
            _setUpState = false;
        }


        //do enemy logic :)
        foreach (var enemy in enemiesInvolved)
        {
            enemy.GetComponent<EnemyLogic>().MoveOneTile(Direction.UP);
        }
        state = BattleState.PLAYERTURN;





        if (_takeDownState)
        {
            //do stuff
            _takeDownState = false;
            _setUpState = true;
        }
    }

    private void Won(Vector3Int[] battleBoundaryTilesLocations)
    {
        if (_setUpState)
        {
            //do stuff
            _setUpState = false;
        }



        BattleOver(battleBoundaryTilesLocations);





        if (_takeDownState)
        {
            //do stuff
            _takeDownState = false;
            _setUpState = true;
        }
    }

    private void Lost(Vector3Int[] battleBoundaryTilesLocations)
    {
        if (_setUpState)
        {
            //do stuff
            _setUpState = false;
        }





        BattleOver(battleBoundaryTilesLocations);





        if (_takeDownState)
        {
            //do stuff
            _takeDownState = false;
            _setUpState = true;
            turnNumber = 0;
        }
    }

    #endregion
    //=====================================
    #region Helper Functions
    //TODO
    private void DecideWhoGoesFirst()
    {
        state = BattleState.PLAYERTURN; //default player goes first
    }

    public void BattleOver(Vector3Int[] boundaryTileLocations)
    {
        _hudsManager.GetComponent<HudsManager>().playerMiniStatsHudActive = false;
        _hudsManager.GetComponent<HudsManager>().battleHudActive = false;

        _tileManager.GetComponent<TileManager>().RemoveTiles(boundaryTileLocations); //delete boundary tiles
    }

    public void FreezeRigidBodies(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
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

    // TODO
    private bool IsWon()
    {
        return false;
    }

    public bool IsLose()
    {
        if (_player.GetComponent<PlayerStats>().currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool AreEnemiesDefeated(GameObject[] enemies)
    {
        int totalDefeated = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyStats>().currentHP <= 0)
            {
                totalDefeated = totalDefeated + 1;
            }
        }
        if (totalDefeated == enemies.Length)
            return true;
        else
            return false;
    }

    #endregion
}
