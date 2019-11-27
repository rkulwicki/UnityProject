using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testGetTile : MonoBehaviour
{
    //List<Tuple<int,int>> tiles
    public Vector2Int tileLocation;
    public Vector2Int[] tileLocations;
    public GameObject grid;

    public Tile tile;
    public Tile[] tiles;
    public Tilemap tilemapFloor;
    public Tilemap tilemapObstacles;

    public Vector2Int center;
    public int size;
    private void Start()
    {
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
    }

    private void LateUpdate()
    {
        //DEBUG vvvvvvvvvvvvvvvvvvvvvvv
        if (Input.GetKey("p"))
        {
            GenerateTile(tileLocation, tile);
        }

        if (Input.GetKey("o"))
        {
            GenerateTiles(tileLocations, tiles);
        }
        if (Input.GetKey("i"))
        {
            GenerateSquareTilesWithCenter(center, size, tiles);
        }
        // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    }

    private void GenerateTile(Vector2Int tileLocation, Tile tile)
    {
        tilemapObstacles.WorldToCell(new Vector3(tileLocation.x, tileLocation.y, 0));
        //this could be useful -> tilemapObstacles.SetTiles
        tilemapObstacles.SetTile(new Vector3Int(tileLocation.x, tileLocation.y, 0), tile);
        //tilemapFloor.
    }

    private void GenerateTiles(Vector2Int[] tileLocations, Tile[] tiles)
    {
        //tilemapObstacles.WorldToCell(new Vector3(tileLocation.x, tileLocation.y, 0));
        tilemapObstacles.SetTiles(ConvertV2ArrayToV3(tileLocations), tiles);
    }

    //creates an open square around a specified center tile using the first
    //
    [Description("Creates an open square around a specified center tile using the first tile in 'Tiles'. ")]
    private void GenerateSquareTilesWithCenter(Vector2Int center, int size, Tile[] tiles)
    {
        //ex. 
        //size = 1, 
        //l = 3, 
        //n = 8
        int l = (size * 2) + 1; //l = length of square
        int n = 4 * (l - 1); //n = number of tiles
        var tileLocations = new Vector2Int[n];
        int c = 0;
        //generate clockwise
        for(int i = 0; i < l - 1; i++)
        {
            var temp = new Vector2Int(center.x - size + i, center.y - size);
            tileLocations[c] = temp;
            c++;
        }
        for (int i = 0; i < l - 1; i++)
        {
            var temp = new Vector2Int(center.x + size, center.y - size + i);
            tileLocations[c] = temp;
            c++;
        }
        for (int i = 0; i < l - 1; i++)
        {
            var temp = new Vector2Int(center.x + size - i, center.y + size);
            tileLocations[c] = temp;
            c++;
        }
        for (int i = 0; i < l - 1; i++)
        {
            var temp = new Vector2Int(center.x - size, center.y + size - i);
            tileLocations[c] = temp;
            c++;
        }
        var firstTileCopied = new Tile[n];
        for (int i = 0; i < n; i++)
        {
            firstTileCopied[i] = tiles[0];
        }
        tilemapObstacles.SetTiles(ConvertV2ArrayToV3(tileLocations), firstTileCopied);
    }

    private Vector3Int[] ConvertV2ArrayToV3(Vector2Int[] v2)
    {
        Vector3Int[] v3 = new Vector3Int[v2.Length];
        for (int i = 0; i < v2.Length; i++)
        {
            Vector2Int tempV2 = v2[i];
            v3[i] = new Vector3Int(tempV2.x, tempV2.y, 0);
        }
        return v3;
    }
}
