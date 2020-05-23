using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using static TilemapFunctions;

public class PlayerMove : Move
{
    public bool isKeyboardMovement;
    private DPadGlobal _DPadGlobal;
    private PlayerStats _playerStats;

    private BattleManager _bm;
    private bool _flag;

    private CapsuleCollider2D _cap2D;

    void Start()
    {
        _cap2D = gameObject.GetComponent<CapsuleCollider2D>();
        _bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        moved = 1;
        _DPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
        isKeyboardMovement = false;
        _playerStats = gameObject.GetComponent<PlayerStats>();
        _flag = true;

        obstaclesTilemaps = GetObstaclesTileMaps();
        floorTilemaps = GetFloorTileMaps();
    }

    void Update()
    {
        OutOfBattleMove(); //if out of battle

        //Listener
        if (_bm.state != BattleState.INACTIVE && _flag)
        {
            _cap2D.enabled = false;

            var v3int = new Vector3Int(Convert.ToInt32(this.transform.position.x),
                Convert.ToInt32(this.transform.position.y),
                Convert.ToInt32(this.transform.position.z));
            StartCoroutine(MoveOverSpeed(this.gameObject, v3int, 3));
            _flag = false;
        }
        if(_bm.state == BattleState.INACTIVE)
        {
            _flag = true;
        }

        //We do nothing if the player is still moving.
        if (isMoving || inActionCooldown || _onExit || !canMove) return;

        //To store move directions.
        int horizontal = 0;
        int vertical = 0;
        //To get move directions
        if (_DPadGlobal.DPadUp)
            MoveOneTile(Direction.UP);
        else if (_DPadGlobal.DPadDown)
            MoveOneTile(Direction.DOWN);
        else if (_DPadGlobal.DPadRight)
            MoveOneTile(Direction.RIGHT);
        else if (_DPadGlobal.DPadLeft)
            MoveOneTile(Direction.LEFT);

        if (isKeyboardMovement)
        {
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));
        }
    }

    private void OutOfBattleMove()
    {
        if (_bm.state == BattleState.INACTIVE)
        {
            _cap2D.enabled = true;
            MoveUp();
            MoveDown();
            MoveRight();
            MoveLeft();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    
    }
}