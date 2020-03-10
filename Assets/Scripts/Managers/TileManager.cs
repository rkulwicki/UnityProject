using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour, IManager
{
    ////List<Tuple<int,int>> tiles
    //public Vector2Int tileLocation;
    //public Vector2Int[] tileLocations;
    public GameObject grid;
    //public bool debug;

    public Tile tile;
    public Tile[] tiles;
    public Tilemap tilemapFloor;
    public Tilemap tilemapObstacles;
    public Tilemap tilemapCarpet;

    public Tile highlightTile;
    //public Vector2Int center;
    //public int size;
    private void Start()
    {
        //get gid, floor, and obstacles
        grid = GameObject.FindGameObjectWithTag("Grid");
        tilemapFloor = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        tilemapObstacles = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
        tilemapCarpet = grid.transform.Find("Carpet").gameObject.GetComponent<Tilemap>();
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

    public void HighlightTiles(Vector3Int[] locs)
    {
        var tempTiles = new Tile[locs.Length];
        for (int i = 0; i < locs.Length ; i++)
        {
            tempTiles[i] = highlightTile;
        }
        tilemapCarpet.SetTiles(locs, tempTiles);
    }

    [Description("Creates an open square around a specified center tile using the first tile in 'Tiles'. ")]
    public Vector3Int[] GenerateSquareTilesWithCenter(Vector2Int center, int size, Tile[] tiles)
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
        var v3TileLocations = ConvertV2ArrayToV3(tileLocations);
        List<Vector3Int> list = new List<Vector3Int>();
        foreach(var tile in v3TileLocations)
        {
            if (!tilemapObstacles.HasTile(tile))
                list.Add(tile);
        }
        var locs = list.ToArray();
        tilemapObstacles.SetTiles(locs, firstTileCopied);
        return locs;

    }

    [Description("Removes tiles under the obstacles tilemap. ")]
    public void RemoveTiles(Vector3Int[] tileLocations)
    {
        foreach (var tileLocation in tileLocations)
        {
            tilemapObstacles.SetTile(tileLocation, null);
        }
    }

    #region Helper Methods

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

    #endregion
}
