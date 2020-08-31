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
    public Tile[,,] tilesTest;
    public Vector3Int testTilePos;
    public bool reset;
    public TileBase tb;

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

        var locs = GetCubePositionsGivenCenter(testTilePos);
        tilesTest = GetSurroundingTilesIn3DSpace(testTilePos);
        SetTilesHere(tb, locs);
        //DebugHighlightSurroundingTiles(tilesTest, reset);
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
        //IMPORTANT NOTE: Sorting order is z
        var grid = this.gameObject.GetComponent<Grid>();
        var locs = GetCubePositionsGivenCenter(tilePos);

        var tiles = new Tile[locs.GetLength(0), locs.GetLength(1), locs.GetLength(2)];

        for (int z = 0; z < locs.GetLength(2); z++) //go through sorting order (z) first then x, then y
        {
            Tilemap tilemap;
            try
            {
                tilemap = tilemapGameObjects.Select(go => go.GetComponent<Tilemap>())
                                                .Where(tm => tm.GetComponent<TilemapRenderer>().sortingOrder == z)
                                                .FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.Log("There is no tilemap with a sorting order of " + z);
                continue;
            }

            for (int x = 0; x < locs.GetLength(0); x++)
            {
                for (int y = 0; y < locs.GetLength(1); y++)
                {
                    if (tilemap != null)
                    {
                        //adjust y position based on 
                        var pos = tilemap.WorldToCell(new Vector3Int(x, y, 0));
                        Tile t = tilemap.GetTile<Tile>(pos);
                        tiles[x, y, z] = t;
                    }
                }
            }
        }

        return tiles;
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
                    locs[x+1, y+1, z+1] = new Vector3Int(pos.x + x, pos.y + y + (z), pos.z + z);
                }
            }
        }

        return locs;
    }

    /// <summary>
    /// For testing. Highlights an array of tiles in pseudo 3d space.
    /// </summary>
    /// <param name="tiles"></param>
    private void DebugHighlightSurroundingTiles(Tile[,,] tiles, bool reset)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    if (tiles[x, y, z] != null)
                    {
                        if (reset)
                        {
                            tiles[x, y, z].color = new Color(1, 1, 1, 1);
                        }
                        if (z == 0)
                            tiles[x, y, z].color = new Color(1, 0.92f, 0.016f, 0.5f); //lower
                        else if (z == 1)
                            tiles[x, y, z].color = new Color(1, 0.92f, 0.016f, 0.75f); //mid
                        else
                            tiles[x, y, z].color = new Color(1, 0.92f, 0.016f, 1); //higher
                    }
                }
            }
        }
    }

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


}
