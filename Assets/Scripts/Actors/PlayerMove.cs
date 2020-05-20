using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerMove : Move
{
    public bool isKeyboardMovement;
    private DPadGlobal _DPadGlobal;
    private PlayerStats _playerStats;

    private BattleManager _bm;
    private bool _flag;
    void Start()
    {
        _bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        moved = 1;
        _DPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
        isKeyboardMovement = false;
        _playerStats = gameObject.GetComponent<PlayerStats>();
        _flag = true;
    }

    void Update()
    {
        OutOfBattleMove(); //if out of battle

        //Listener
        if (_bm.state != BattleState.INACTIVE && _flag)
        {
            SmoothPositionToTile(1);
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
        if(_bm.state == BattleState.INACTIVE)
        {
            //if tile is obstacle then can't move

            MoveUp();
            MoveDown();
            MoveRight();
            MoveLeft();
        }
    }

    private void SmoothPositionToTile(float time)
    {
        var v3int = new Vector3Int(Convert.ToInt32(transform.position.x),
                                Convert.ToInt32(transform.position.y),
                                Convert.ToInt32(transform.position.z));
        Vector3.MoveTowards(transform.position, v3int, time);
    }
}