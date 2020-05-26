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

    public static int GetOrderInLayerOfFloorBelow(Vector3 pos)
    {
        Tilemap[] floorTilemaps = GetFloorTileMaps();
        Tilemap[] obstaclesTilemaps = GetObstaclesTileMaps();
        foreach (var floorTilemap in floorTilemaps)
        {
            var roundedPos = new Vector3Int(Convert.ToInt32(pos.x), Convert.ToInt32(pos.y), Convert.ToInt32(pos.z));
            if (floorTilemap.HasTile(roundedPos))
                return floorTilemap.gameObject.GetComponent<TilemapRenderer>().sortingOrder;
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
