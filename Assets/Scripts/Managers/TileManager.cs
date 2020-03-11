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

    public void HighlightTiles(Vector3Int[] locs)
    {
        var tempTiles = new Tile[locs.Length];
        for (int i = 0; i < locs.Length ; i++)
        {
            tempTiles[i] = highlightTile;
        }
        tilemapCarpet.SetTiles(locs, tempTiles);
    }

    /// <summary>
    /// Creates an open square around a specified center tile using the first tile in 'Tiles'.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Removes tiles under the obstacles tilemap. 
    /// </summary>
    /// <param name="tileLocations"></param>
    public void RemoveTiles(Vector3Int[] tileLocations)
    {
        foreach (var tileLocation in tileLocations)
        {
            tilemapObstacles.SetTile(tileLocation, null);
        }
    }

    public Vector3Int[] GenerateBoundaryPosFromArea(Vector3Int[] area)
    {
        List<Vector3Int> list = new List<Vector3Int>();
        var surroundingPositions = new Vector3Int[8]
        {
            new Vector3Int(-1,1,0), new Vector3Int(0,1,0), new Vector3Int(1,1,0),
            new Vector3Int(-1,0,0),                         new Vector3Int(1,0,0),
            new Vector3Int(-1,-1,0),new Vector3Int(0,-1,0), new Vector3Int(1,-1,0),
        };
        foreach(var pos in area)
        {
            for(int i = 0; i < 8; i++) //check surrounding tiles
            {
                bool canAdd = true;
                var possibleBoundaryPos = pos + surroundingPositions[i];

                foreach (var otherPos in list)
                {
                    if (otherPos == possibleBoundaryPos) { //if found in "list"
                        canAdd = false;
                        break;
                    }
                }

                if (canAdd)
                {
                    foreach (var otherPos in area)
                    {
                        if (otherPos == possibleBoundaryPos) //if found "area"
                        {
                            canAdd = false;
                            break;
                        }
                    }
                }

                if (canAdd)
                    list.Add(possibleBoundaryPos);
            }
        }
        return list.ToArray();
    }

    public void GenerateBoundaryFromArea(Vector3Int[] positions, Tile tile)
    {
        //make sure there isn't anything on the obstacles layer
        List<Vector3Int> listPos = new List<Vector3Int>();
        foreach (var pos in positions)
        {
            if (!tilemapObstacles.HasTile(pos))
                listPos.Add(pos);
        }

        var newPosArray = listPos.ToArray();
        var tiles = new Tile[newPosArray.Length];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = tile;
        }
        //TODO!!!
        tilemapObstacles.SetTiles(newPosArray, tiles);

        /*
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
         */
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
