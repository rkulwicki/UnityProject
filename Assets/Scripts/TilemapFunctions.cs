using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

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
