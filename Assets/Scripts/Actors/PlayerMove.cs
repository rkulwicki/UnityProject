﻿using System.Collections;
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
    public bool canMoveUp, canMoveRight, canMoveLeft, canMoveDown;
    private DPadGlobal _DPadGlobal;
    private PlayerStats _playerStats;

    private BattleManager _bm;
    private bool _flag;

    private CapsuleCollider2D _cap2D;

    private float _jitterBuffer;

    private Vector3 _offset;

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

        _jitterBuffer = 0.1f;
        _offset = new Vector3(_cap2D.offset.x, _cap2D.offset.y, 0);

        canMoveDown = true;
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
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
            var tAdjusted = new Vector2(transform.position.x + _cap2D.offset.x, transform.position.y + _cap2D.offset.y);

            _cap2D.enabled = true;
            if (CanMoveThisDirection(Direction.UP, tAdjusted, (_cap2D.size.y / 2) + _jitterBuffer) && canMoveUp)
                MoveUp();

            if (CanMoveThisDirection(Direction.DOWN, tAdjusted, (_cap2D.size.y / 2) + _jitterBuffer) && canMoveDown)
                MoveDown();

            if (CanMoveThisDirection(Direction.RIGHT, tAdjusted, (_cap2D.size.x / 2) + _jitterBuffer) && canMoveRight)
                MoveRight();

            if (CanMoveThisDirection(Direction.LEFT, tAdjusted, (_cap2D.size.x / 2) + _jitterBuffer) && canMoveLeft)
                MoveLeft();
        }
    }

    private bool CanMoveThisDirection(Direction dir, Vector2 center, float distance)
    {

        if (dir == Direction.UP)
        {
            // Cast a ray up.
            RaycastHit2D hit = Physics2D.Raycast(center, Vector2.up, distance);
            RaycastHit2D projLandHit = Physics2D.Raycast(transform.GetComponent<Jump>().projectedLanding, Vector2.up, distance);
            if (hit.collider != null && !IsOnWallTilemap(transform.position + _offset) ||
                (IsOnWallTilemap(transform.position + _offset) && projLandHit.collider != null))
            { // if you hit something, but also
              // if it's not in front of a wall
                Debug.Log("cannot move up");
                return false;
            }
        }
        else if (dir == Direction.RIGHT)
        {
            // Cast a ray right.
            RaycastHit2D hit = Physics2D.Raycast(center, Vector2.right, distance);
            if (hit.collider != null && !IsOnWallTilemap(transform.position + _offset))
                return false;
        }
        else if (dir == Direction.DOWN)
        {
            // Cast a ray down.
            RaycastHit2D hit = Physics2D.Raycast(center, Vector2.down, distance);
            RaycastHit2D projLandHit = Physics2D.Raycast(center, Vector2.up, distance);
            if (hit.collider != null && !IsOnWallTilemap(transform.position + _offset) &&
                hit.collider.gameObject.name != "Wall" ||
                (IsOnWallFrontTilemap(transform.position + _offset) && projLandHit.collider != null)) //if you're going down, you're above the wall. Can move
                return false;
        }
        else if (dir == Direction.LEFT)
        {
            // Cast a ray left.
            RaycastHit2D hit = Physics2D.Raycast(center, Vector2.left, distance);
            if (hit.collider != null && !IsOnWallTilemap(transform.position + _offset))
                return false;
        }
        return true;
    }

    private Vector2 GetEdge(Direction dir, Vector2 offset, Vector2 size)
    {
        if(dir == Direction.UP)
        {
            return new Vector2(offset.y + (size.y/2), offset.x);
        }
        else if (dir == Direction.DOWN)
        {
            return new Vector2(offset.y - (size.y / 2), offset.x);
        }
        else if (dir == Direction.LEFT)
        {
            return new Vector2(offset.y, offset.x - (size.x / 2));
        }
        else if (dir == Direction.RIGHT)
        {
            return new Vector2(offset.y, offset.x + (size.x / 2));
        }
        return new Vector2(0, 0);
    }

}