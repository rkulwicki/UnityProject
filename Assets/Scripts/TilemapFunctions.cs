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

}
