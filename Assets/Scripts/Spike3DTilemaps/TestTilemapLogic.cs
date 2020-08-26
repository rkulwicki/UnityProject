using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

//To be attached to Grid game object

public class TestTilemapLogic : MonoBehaviour
{
    private GameObject[] tilemapGameObjects; 
    public Vector3 _pseudo3DPosition;

    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in transform)
        {
            list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
    }

    void Update()
    {
        _pseudo3DPosition = GameObject.Find("TestPlayer").GetComponent<TestPlayer>().pseudo3DPosition;
        ChangeTilemapLayerByPlayerHeight();
    }

    private void ChangeTilemapLayerByPlayerHeight()
    {
        foreach (var tm in tilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "FloorBelow" && _pseudo3DPosition.z < tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "FloorAbove";

            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "FloorAbove" && _pseudo3DPosition.z >= tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "FloorBelow";
        }
    }

    /// <summary>
    /// Given the sorting order as the Z coordinate, returns a 3d array of tiles surrounding
    /// a given tile in the pseudo 3d space.
    /// </summary>
    /// <returns>Tile[,,]</returns>
    private Tile[,,] GetSurroundingTilesIn3DSpace(Vector3Int tilePos)
    {

        var locs = GetCubePositionsGivenCenter(tilePos);

        var tiles = new Dictionary<int, Tile[][]>();
        for(int i = sortingOrder - 1; i <= sortingOrder + 1; i++) //calc for each z plane (or sorting order
        {
            Tilemap tilemap;
            try
            {
                tilemap = tilemapGameObjects.Select(x => x.GetComponent<Tilemap>())
                                                .Where(y => y.GetComponent<TilemapRenderer>().sortingOrder == i)
                                                .FirstOrDefault();
            }
            catch(Exception e)
            {
                Debug.Log("There is no tilemap with a sorting order of " + i);
            }

            
            tilemap.GetTile()
        }
    }

    private Vector3Int[,,] GetCubePositionsGivenCenter(Vector3Int pos)
    {
        var locs = new Vector3Int[3, 3, 3];

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    locs[x+1, y+1, z+1] = new Vector3Int(pos.x + x, pos.y + y, pos.z + z);
                }
            }
        }

        return locs;
    }
}
