using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState { INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleManager : MonoBehaviour, IManager
{
    public int turnNumber, blocksLeftToMove;

    public GameObject selectorPrefab;

    public GameObject grid;
    public GameObject tilemapFloor;
    public GameObject tilemapObstacles;

    public GameObject[] playersInvolved;
    public GameObject[] enemiesInvolved;

    public GameObject currentEnemy;

    public BattleState state;

    public int secondsBetweenEnemy = 1;

    public GameObject initiatedEnemy; //comes from BattleTrigger.cs
    public Vector3Int battleBlocksPos; //comes from BattleTrigger.cs

    private GameObject _hudsManager;
    private GameObject _tileManager;
    private ChooseObjectWithBools _chooseObjectWithBools;
    private PlayerBattleGlobal _playerBattleGlobal;
    private GameObject _player;

    private Vector3Int[] _battleBoundaryTilesLocations;

    private bool _setUpState, _takeDownState, _isWon, _isLose, _movingAction, _attackActionDone, _moveActionDone;
    private int _blockSpeed, _moved;

    public bool testMove;
    void Start()
    {
        state = BattleState.INACTIVE;
        _hudsManager = GameObject.FindGameObjectWithTag("HudsManager");
        _tileManager = GameObject.FindGameObjectWithTag("TileManager");
        //_chooseObjectWithKeys = gameObject.GetComponent<ChooseObjectWithKeys>();
        _chooseObjectWithBools = gameObject.GetComponent<ChooseObjectWithBools>();
        _playerBattleGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<PlayerBattleGlobal>();
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

        if(initiatedEnemy == null)
            initiatedEnemy = enemiesInvolved[0];

        var battleArea = initiatedEnemy.GetComponent<EnemyStats>().battleArea;

        var enemyPosRounded = new Vector3Int(Convert.ToInt32(initiatedEnemy.transform.position.x),
                                             Convert.ToInt32(initiatedEnemy.transform.position.y),
                                             Convert.ToInt32(initiatedEnemy.transform.position.z));

        var pos = _tileManager.GetComponent<TileManager>().GenerateBoundaryPosFromArea(battleArea, enemyPosRounded); //here is the magic
        var battleBoundaryTilesLocations =  _tileManager.GetComponent<TileManager>().PlaceTilesIfEmpty(pos, initiatedEnemy.GetComponent<EnemyStats>().battleBoundaryTile);
        return battleBoundaryTilesLocations;
    }

    private void StartBattle()
    {
        _battleBoundaryTilesLocations = BeforeStart();
        _player.GetComponent<PlayerMove>().canMove = false;

        //TODO: make some sort of "button pressed" function or something to do this logic
        _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .moveButton.image.color = Color.white;
        _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .attackButton.image.color = Color.white;

        foreach (var enemy in enemiesInvolved) //make them in "BattleMode"
        {
            enemy.transform.Find("BattleTrigger").GetComponent<BoxCollider2D>().enabled = false; //turn off battle triggers for enemies
            enemy.GetComponent<DemonEnemyBattleAI>().beginLogic = true; //need to find "EnemyLogic" subclasses... hmm...
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
            _movingAction = false;
            _attackActionDone = false;
            _moveActionDone = false;
        }

        //                      ATTACK
        // ==================================================
        if (_playerBattleGlobal.AttackButton && !_attackActionDone) //atack false, move true
        {
            _playerBattleGlobal.AttackButton = false;
            
            
            
            
            //TODO: first choose type of attack (goes here before StartChoose)
            //1. display list of attacks. Probably stored in a list related to player stats?
            //2. cycle through the list, whichever is highlighted, showing the ground highlighted.
            //2.5 is allowed to choose attack if enemy is in highlighted box.
            //3. do attack and continue to "_chooseObjectWithBools.result != null"




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
            _attackActionDone = true;
            if (!_moveActionDone)
            {
                _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .attackButton.image.color = Color.gray;
                _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true;
                //turn button off or something?
            }
        }

        //                      MOVE
        // ==================================================
        if (_playerBattleGlobal.MoveButton && !_moveActionDone)
        {
            _playerBattleGlobal.MoveButton = false;
            _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = false;
            _blockSpeed = _player.GetComponent<PlayerStats>().blockSpeed;
            _player.GetComponent<PlayerMove>().canMove = true;
            _moved = _player.GetComponent<Move>().moved;
            _movingAction = true;
        }
        if (_movingAction)
        {
            MovingAction();
        }
        if (_moveActionDone && _movingAction)
        {
            _player.GetComponent<PlayerMove>().canMove = false;
            _movingAction = false;
            if (!_attackActionDone)
            {
                _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .moveButton.image.color = Color.gray;
                _hudsManager.GetComponent<HudsManager>().playerBattleActionHudActive = true;
                //turn button off or something?
            }
        }

        //                  BATTLE OVER
        // ==================================================
        if (ActorsHPZero(playersInvolved) || enemiesInvolved.Length == 0)
        {
            _isLose = true;
            _takeDownState = true;
        }
        if (ActorsHPZero(enemiesInvolved) || enemiesInvolved.Length == 0)
        {
            _isWon = true;
            _moveActionDone = true;
        }

        //                  END OF TURN
        // ==================================================
        if (_attackActionDone && _moveActionDone)
            _takeDownState = true;

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
            _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .attackButton.image.color = Color.white;
            _hudsManager.GetComponent<HudsManager>()
                            .playerBattleActionHud.GetComponent<PlayerBattleButtons>()
                            .moveButton.image.color = Color.white;
            _moveActionDone = false;
            _attackActionDone = false;
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
            StartCoroutine(ForEachEnemyTurn(secondsBetweenEnemy, enemiesInvolved)); //do once
        }




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
            _takeDownState = false;
            _setUpState = true;
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
        state = BattleState.INACTIVE;
        turnNumber = 0;
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
            enemy.GetComponent<DemonEnemyBattleAI>().beginTurn = true;
            yield return new WaitForSeconds(sec);
        }
        _takeDownState = true;
    }

    public void MovingAction()
    {
        if (_blockSpeed > 0)
        {
            if(_moved != _player.GetComponent<PlayerMove>().moved)
            {
                //take a movement
                _moved = _player.GetComponent<PlayerMove>().moved;
                Debug.Log(_blockSpeed);
                _blockSpeed -= 1;
            }
        }
        else
        {
            _moveActionDone = true;
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

    public void EndPlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        _takeDownState = true;
    }

    #endregion
}
