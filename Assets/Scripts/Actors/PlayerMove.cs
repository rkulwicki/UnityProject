using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMove :Move
{
    public bool isKeyboardMovement;
    private DPadGlobal _DPadGlobal;
    private PlayerStats _playerStats;
    void Start()
    {
        moved = 1;
        _DPadGlobal = GameObject.FindGameObjectWithTag("GlobalInputs").GetComponent<DPadGlobal>();
        isKeyboardMovement = false;
        _playerStats = gameObject.GetComponent<PlayerStats>();
    }

    void Update()
    {
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
            //todo basically impliment move one tile
        }
    }
}