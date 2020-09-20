using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
using UnityEditor;

//To be attached to Grid game object

public class TestTilemapLogic : MonoBehaviour
{
    private GameObject[] tilemapGameObjects; 
    public Vector3 _pseudo3DPosition;
    public TileBase[,,] tilesTest;
    public Vector3Int testTilePos;
    public bool reset;
    public TileBase tb;

    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "Tilemap")
                list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
    }

    void Update()
    {
        _pseudo3DPosition = GameObject.Find("TestPlayer").GetComponent<TestPlayer>().pseudo3DPosition;
        ChangeTilemapLayerByPlayerHeight();

        var locsWO = GetCubePositionsGivenCenterWithTiles(testTilePos);
        SetTilesHere(tb, locsWO);
    }

    /// <summary>
    /// Changes the sorting order of tilemaps based on the pseudo z height of the player.
    /// </summary>
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
    /// Mostly for testing - replaces individual tiles give a 3d array of tile positions
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="pos"></param>
    private void SetTilesHere(TileBase tb, Vector3Int[,,] pos)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    if (pos[x, y, z] != null)
                    {
                        foreach (var tm in tilemapGameObjects)
                        { 
                            if (tm.GetComponent<TilemapRenderer>().sortingOrder == pos[x, y, z].z)
                            {
                                tm.GetComponent<Tilemap>().SetTile(new Vector3Int(pos[x,y,z].x, pos[x, y, z].y, 0), tb);
                            }
                        
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Given a center, returns the integer values  of the 8 tiles surrounding the center and including
    /// the center.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3Int[,,] GetCubePositionsGivenCenter(Vector3Int pos)
    {
        var locs = new Vector3Int[3, 3, 3];

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    locs[x + 1, y + 1, z + 1] = new Vector3Int(pos.x + x, pos.y + y + (z), pos.z + z);
                }
            }
        }

        return locs;
    }

    /// <summary>
    /// Given a center, returns the integer values  of the 8 tiles surrounding the center and including
    /// the center where there are currently tiles present in the Grid that this script is attached to.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3Int[,,] GetCubePositionsGivenCenterWithTiles(Vector3Int pos)
    {
        var locs = new Vector3Int[3, 3, 3];

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    var newTilePos = new Vector3Int(pos.x + x, pos.y + y + (z), pos.z + z);

                    var tilemap = tilemapGameObjects.Select(go => go.GetComponent<Tilemap>())
                                .Where(tm => tm.GetComponent<TilemapRenderer>().sortingOrder == newTilePos.z)
                                .First();

                    if (tilemap == null)
                        continue;

                    var t = tilemap.GetTile(new Vector3Int(newTilePos.x, newTilePos.y, 0));
                    if (t != null)
                    {
                        locs[x + 1, y + 1, z + 1] = newTilePos;
                    }
                }
            }
        }

        return locs;
    }

}
