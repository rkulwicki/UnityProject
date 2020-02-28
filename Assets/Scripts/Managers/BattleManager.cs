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

    public int secondsBetweenEnemy = 1;

    private GameObject _hudsManager;
    private GameObject _tileManager;
    private ChooseObjectWithBools _chooseObjectWithBools;
    private PlayerBattleGlobal _playerBattleGlobal;
    private DPadGlobal _dPadGlobal;
    private GameObject _player;

    private Vector3Int[] _battleBoundaryTilesLocations;

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
                Won();
            }
            else if (state == BattleState.LOST)
            {
                Lost();
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
        _battleBoundaryTilesLocations = BeforeStart();
        _player.GetComponent<PlayerMove>().canMove = false;
        foreach (var enemy in enemiesInvolved) //make them in "BattleMode"
        {
            enemy.GetComponent<DemonEnemyLogic>().beginLogic = true; //need to find EnemyLogic subclasses... hmm...
        }
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
            _playerBattleGlobal.AttackButton = false;
            //TODO: first choose type of attack (goes here before StartChoose)
            _chooseObjectWithBools.StartChoose(selectorPrefab, enemiesInvolved);
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
        }

        if (_chooseObjectWithBools.result != null) //after chosen
        {
            currentEnemy = _chooseObjectWithBools.currentObject;
            //TODO:
            //Here. Insert logic for attacking, given the attack and the chosen enemy.    
            var baseD = _player.GetComponent<PlayerStats>().baseDamage;
            var curEnStats = currentEnemy.GetComponent<EnemyStats>();
            var playerActions = _player.GetComponent<PlayerBattleActions>();
            playerActions.Attack(baseD, curEnStats);
            //^^ currently just the enemy and attack is base damage. Attack choice will be later.

            _chooseObjectWithBools.result = null; //reset the choice.
            _takeDownState = true;
        }
        // ==================================================

        if (_playerBattleGlobal.MoveButton)
        {
            _playerBattleGlobal.MoveButton = false;
            //move
            //_player.GetComponent<PlayerMove>().canMove = true;
            //then ---> _player.GetComponent<PlayerMove>().canMove = false;
            _takeDownState = true;
        }



        //Have we won or lost?
        if (ActorsHPZero(playersInvolved) || enemiesInvolved.Length == 0)
        {
            _isLose = true;
            _takeDownState = true;
        }
        if (ActorsHPZero(enemiesInvolved) || enemiesInvolved.Length == 0)
        {
            _isWon = true;
            _takeDownState = true;
        }
            



        if (_takeDownState)
        {
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
            if (_isWon)
                state = BattleState.WON;
            else if (_isLose)
                state = BattleState.LOST;
            else
            {
                state = BattleState.ENEMYTURN;
            }
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

        //need to find EnemyLogic subclasses... hmm...
        StartCoroutine(ForEachEnemyTurn(secondsBetweenEnemy, enemiesInvolved));

        //WAIT UNTIL ENEMIES ARE DONE vvvvv TODO!!!!
        _takeDownState = true;




        if (_takeDownState)
        {
            //do stuff
            state = BattleState.PLAYERTURN;
            _takeDownState = false;
            _setUpState = true;
        }
    }

    private void Won()
    {
        if (_setUpState)
        {
            //do stuff
            _setUpState = false;
        }



        BattleOver();





        if (_takeDownState)
        {
            //do stuff
            _player.GetComponent<PlayerMove>().canMove = true;
            _takeDownState = false;
            _setUpState = true;
        }
    }

    private void Lost()
    {
        if (_setUpState)
        {
            //do stuff
            _setUpState = false;
        }





        BattleOver();





        if (_takeDownState)
        {
            //do stuff
            _player.GetComponent<PlayerMove>().canMove = true;
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

    public void BattleOver()
    {
        _hudsManager.GetComponent<HudsManager>().playerMiniStatsHudActive = false;
        _hudsManager.GetComponent<HudsManager>().battleHudActive = false;

        _tileManager.GetComponent<TileManager>().RemoveTiles(_battleBoundaryTilesLocations); //delete boundary tiles
        _player.GetComponent<PlayerMove>().canMove = true;
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

    public IEnumerator ForEachEnemyTurn(int sec, GameObject[] enemies)
    {
        foreach (var enemy in enemiesInvolved)
        {
            //TODO: enemy.GetComponent<DemonEnemyLogic>().beginTurn = true; //need to find EnemyLogic subclasses... hmm...
            enemy.GetComponent<DemonEnemyLogic>().beginTurn = true;
            yield return new WaitForSeconds(sec);
        }
    }

    private bool ActorsHPZero(GameObject[] actors)
    {
        int c = 0;
        foreach (var actor in actors)
        {
            if (actor.GetComponent<ActorStats>().currentHP <= 0)
                c++;
        }
        if (c == actors.Length)
            return true;
        else
            return false;
    }

    #endregion
}
