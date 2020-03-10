using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightBlocks : TileManager
{
    public bool testHighlight = false;
    public Vector3Int[] tileLocations;

    private void Start()
    {
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
        tilemapCarpet = grid.transform.Find("Carpet").gameObject.GetComponent<Tilemap>();
        tileLocations = new Vector3Int[3];
        tileLocations[0] = new Vector3Int(1, 1, 0);
        tileLocations[1] = new Vector3Int(1, 0, 0);
        tileLocations[2] = new Vector3Int(1, -1, 0);
        
    }
    //TEST
    private void Update()
    {
        if (testHighlight)
        {
            HighlightTiles(tileLocations);
            
        }
    }
}
