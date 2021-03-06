﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AddCollidersToObstacleTilemaps : MonoBehaviour
{

    public GameObject[] tilemapGameObjects;  //arrays are faster
    public float jumpBuffer = 0.2f;
    private Jump _jump;
    private MovementInfo _moveInfo;

    void Start()
    {
        
        var list = new List<GameObject>();
        foreach(Transform child in transform)
        {
            list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
        _moveInfo  = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTilemapLayerByPlayerHeight();

        EnableCollidersForObstaclesTilemaps();
    }

    private void ChangeTilemapLayerByPlayerHeight()
    {
        foreach (var tm in tilemapGameObjects)
        {

            //TODO
            //change this so it only affects tilemaps that are one layer above the player
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Floor" &&
                _moveInfo.GlobalPosition.z - jumpBuffer < tm.GetComponent<TilemapRenderer>().sortingOrder)
            {
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "Obstacles";
            }

            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Obstacles" &&
                _moveInfo.GlobalPosition.z + jumpBuffer > tm.GetComponent<TilemapRenderer>().sortingOrder)
            {
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "Floor";
            }

        }
    }

    private void EnableCollidersForObstaclesTilemaps()
    {
        foreach (var tm in tilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Obstacles" &&
                !tm.GetComponent<TilemapCollider2D>().enabled)
            {
                tm.GetComponent<TilemapCollider2D>().enabled = true;
            }
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Floor" &&
                tm.GetComponent<TilemapCollider2D>().enabled)
            {
                tm.GetComponent<TilemapCollider2D>().enabled = false;
            }
        }
    }



}
