using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

//todo inhearit 
//using Actors.Move;

public class EnemyMoveAI : Move
{
    public bool isBattle;

    protected EnemyStats enemyStats;
    protected GameObject player;
    protected bool inMoveTowardsActor;
    protected bool inMoveTowardsActorWIP;

    protected int stepsCounter = 0;
    protected EnemyBattleActions enemyActions;

    protected BattleManager battleManager;

    // Use this for initialization
    void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        enemyActions = gameObject.AddComponent<EnemyBattleActions>();
        enemyStats = gameObject.GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        grid = GameObject.FindGameObjectWithTag("Grid");
        groundTilemap = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        obstaclesTilemap = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
        inMoveTowardsActor = false;
    }

    protected void MoveRandomDirectionAvoidObstacles()
    {
        var directions = WhatTilesAreOpen(transform.position);
        MoveOneTile(RandomDirection(directions));
    }

    protected Direction[] WhatTilesAreOpen(Vector3 v3)
    {
        var directions = new Direction[4] { Direction.UP, Direction.RIGHT, Direction.LEFT, Direction.DOWN };
        var list = new List<Direction>();
        foreach (var dir in directions)
        {
            //check if can move this way
            Vector2 startCell = transform.position;
            Vector2 targetCell = new Vector2();
            if (dir == Direction.UP) targetCell = startCell + new Vector2(0, 1);
            else if (dir == Direction.RIGHT) targetCell = startCell + new Vector2(1, 0);
            else if (dir == Direction.DOWN) targetCell = startCell + new Vector2(0, -1);
            else if (dir == Direction.LEFT) targetCell = startCell + new Vector2(-1, 0);

            bool hasObstacleTile = getCell(obstaclesTilemap, targetCell) != null; //if target Tile has an obstacle
            //Debug.Log(dir.ToString() + " " + hasObstacleTile.ToString());
            if (!hasObstacleTile)
            {
                list.Add(dir);
            }
        }
        
        return list.ToArray();
    }

    protected Direction RandomDirection()
    {
        var rnd = new System.Random();
        int rand = rnd.Next(1, 5);
        var randRounded = Convert.ToInt32(rand);
        switch (randRounded)
        {
            case 1:
                return Direction.UP;
            case 2:
                return Direction.RIGHT;
            case 3:
                return Direction.DOWN;
            case 4:
                return Direction.LEFT;
            default:
                return Direction.DOWN;
        }
    }

    protected Direction RandomDirection(Direction[] directions)
    {
        var rnd = new System.Random();
        int rand = rnd.Next(0, directions.Length);
        var randRounded = Convert.ToInt32(rand);
        return directions[rand];
    }

    protected void Patrol()
    {
        //todo
    }

}
