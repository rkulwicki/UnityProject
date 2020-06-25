using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

public class TilemapFunctions
{
    public static Tilemap[] GetObstaclesTileMaps()
    {
        return GetTilemapsBySortingLayer("Obstacles");
    }

    public static Tilemap[] GetFloorTileMaps()
    {
        return GetTilemapsBySortingLayer("Floor");
    }

    public static Tilemap[] GetCarpetTileMaps()
    {
        return GetTilemapsBySortingLayer("Carpet");
    }

    public static Tilemap[] GetFrontTileMaps()
    {
        return GetTilemapsBySortingLayer("Front");
    }

    public static GameObject[] GetAllTileMapsObjects()
    {
        var grid = GameObject.FindGameObjectWithTag("Grid");

        var list = new List<GameObject>();
        foreach (Transform child in grid.transform)
        {
            list.Add(child.gameObject);
        }
        return list.ToArray();
    }

    public static bool IsOnWallTilemap(Vector3 pos)
    {
        var roundedPos = new Vector3Int(Convert.ToInt32(pos.x), Convert.ToInt32(pos.y), Convert.ToInt32(pos.z));
        var tilemaps = GetTilemapsBySortingLayer("Wall");
        if (tilemaps == null)
            return false;
        foreach (var tilemap in tilemaps)
        {
            if (tilemap.HasTile(roundedPos))
                return true;
        }
        return false;
    }

    public static int GetOrderOfTilemapAtPosition(Vector3 pos)
    {
        GameObject[] objs = GetAllTileMapsObjects();
        foreach (var obj in objs)
        {
            var tilemap = obj.GetComponent<Tilemap>();   
            var roundedPos = new Vector3Int(Convert.ToInt32(pos.x), Convert.ToInt32(pos.y), Convert.ToInt32(pos.z));
            if (tilemap.HasTile(roundedPos))
                return tilemap.gameObject.GetComponent<TilemapRenderer>().sortingOrder;
        }
        return -99;
    }

    private static Tilemap[] GetTilemapsBySortingLayer(string sortingLayer)
    {
        var tilemaps = new List<Tilemap>();

        var grid = GameObject.FindGameObjectWithTag("Grid");

        //check each tilemap in grid to see if it is under the sorting layer sortingLayer
        foreach(Transform child in grid.transform)
        {
            if (child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == sortingLayer)
                tilemaps.Add(child.gameObject.GetComponent<Tilemap>());
        }

        return tilemaps.ToArray();
    }

    private static Tilemap[] GetTilemapsByOrder(int order)
    {
        var tilemaps = new List<Tilemap>();

        var grid = GameObject.FindGameObjectWithTag("Grid");

        //check each tilemap in grid to see if it is under the sorting layer sortingLayer
        foreach (Transform child in grid.transform)
        {
            if (child.gameObject.GetComponent<TilemapRenderer>().sortingOrder == order)
                tilemaps.Add(child.gameObject.GetComponent<Tilemap>());
        }

        return tilemaps.ToArray();
    }

    private static Tilemap[] GetTilemapsByOrderInLayer(string sortingLayer, int order)
    {
        var tilemaps = new List<Tilemap>();

        var grid = GameObject.FindGameObjectWithTag("Grid");

        //check each tilemap in grid to see if it is under the sorting layer sortingLayer
        foreach (Transform child in grid.transform)
        {
            if (child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == sortingLayer &&
                child.gameObject.GetComponent<TilemapRenderer>().sortingOrder == order)
                tilemaps.Add(child.gameObject.GetComponent<Tilemap>());
        }

        return tilemaps.ToArray();
    }


    //  WIP


        todo
    /// <summary>
    /// Takes in a position (x, y) and the height (z) and determines what tilemap floor they directly above.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetProjectedLanding(Vector3 pos, float zHeight)
    {
        var floorsBelow = (int)Math.Truncate(zHeight);
        for (var i = floorsBelow; i > 0; i--) //check each floor if it is the potential floor straight below.
        {
            //"i" represents the order in layer of they tilemap. Aka, the floor.

            //check only the tilemap which is the floor we are currently looking at
            var iLayerTilemaps = GetTilemapsByOrder(i);

            foreach (var tilemap in iLayerTilemaps)
            {
                var distFromTilemap = zHeight - (float)i; 
                //need to truncate because "HasTile" only takes V3Int
                var tileLandedOn = new Vector3Int((int)Math.Truncate(pos.x),
                                                  (int)Math.Truncate(pos.y - distFromTilemap),
                                                  0);
                
                var hasTile = tilemap.GetComponent<Tilemap>().HasTile(tileLandedOn);

                //if found a tile at a certain tilemap at a given landing position then return the landing position
                if (hasTile)
                    return new Vector3(pos.x, pos.y - distFromTilemap, pos.z);
            }
        }

        //else
        return pos;
    }

}
