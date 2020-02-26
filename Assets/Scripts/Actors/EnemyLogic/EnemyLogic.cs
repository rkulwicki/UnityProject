using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyLogic : Move
{

    //EnemeyLogic class will contain a base for how enemies will behave
    //(1) Movement Patterns
    //(2) Attack Patterns

    private EnemyStats _enemyStatsReference;
    private GameObject _player;

    void Start()
    {
        _enemyStatsReference = gameObject.GetComponent<EnemyStats>();
        _player = GameObject.FindGameObjectWithTag("Player");
        grid = GameObject.FindGameObjectWithTag("Grid");
        groundTilemap = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        obstaclesTilemap = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
    }
}