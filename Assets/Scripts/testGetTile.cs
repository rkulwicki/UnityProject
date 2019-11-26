using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testGetTile : MonoBehaviour
{
    //List<Tuple<int,int>> tiles
    public Vector2Int tileLocation;
    public GameObject grid;

    public Tile tile;
    public Tilemap tilemapFloor;
    public Tilemap tilemapObstacles;

    private void Start()
    {
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
    }

    private void LateUpdate()
    {
        if (Input.GetKey("p"))
        {
            GenerateTile();
        }
    }

    private void GenerateTile()
    {
        tilemapObstacles.WorldToCell(new Vector3(tileLocation.x, tileLocation.y, 0));
        //this could be useful -> tilemapObstacles.SetTiles
        tilemapObstacles.SetTile(new Vector3Int(tileLocation.x, tileLocation.y, 0), tile);
        //tilemapFloor.
    }
}
