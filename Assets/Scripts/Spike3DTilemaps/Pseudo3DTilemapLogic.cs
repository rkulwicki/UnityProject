using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
using static Globals;

//To be attached to Grid game object

public class Pseudo3DTilemapLogic : MonoBehaviour
{
    private GameObject[] tilemapGameObjects; 
    public Vector3 _pseudo3DPosition;
    public TileBase[,,] tilesTest;
    public Vector3Int testTilePos;
    public bool reset;
    public TileBase tb;
    public float playerHeightBuffer;
    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == TilemapTag)
                list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
    }

    void Update()
    {
        _pseudo3DPosition = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Pseudo3DPosition>().pseudo3DPosition;
        ChangeTilemapLayerByPlayerHeight();

        //var locsWO = GetCubePositionsGivenCenterWithTiles(testTilePos);
        //SetTilesHere(tb, locsWO);
    }

    /// <summary>
    /// Changes the sorting order of tilemaps based on the pseudo z height of the player.
    /// </summary>
    private void ChangeTilemapLayerByPlayerHeight()
    {
        foreach (var tm in tilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == FloorBelowTag && _pseudo3DPosition.z + playerHeightBuffer < tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = FloorAboveTag;

            else if (tm.GetComponent<TilemapRenderer>().sortingLayerName == FloorAboveTag && _pseudo3DPosition.z >= tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = FloorBelowTag;
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

    /// <summary>
    /// Takes in a position (x, y) and the height (z) and determines what tilemap floor they directly above.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetProjectedLandingFromPseudo3DPosition(Vector3 pseudo3DPos)
    {
        var floorsBelow = (int)Math.Truncate(pseudo3DPos.z);
        for (var i = floorsBelow; i > 0; i--) //check each floor if it is the potential floor straight below. 
        {                                     //starting with highest floor first 
            var iLayerTilemaps = GetTilemapsByOrder(i);

            if (iLayerTilemaps.Count() <= 0)
                continue;

            foreach (var tilemap in iLayerTilemaps)
            {
                //need to truncate because "HasTile" only takes V3Int
                var tileLandedOn = new Vector3Int((int)Math.Floor(pseudo3DPos.x),
                                                  (int)Math.Floor(pseudo3DPos.y + i),
                                                  0);

                var hasTile = tilemap.GetComponent<Tilemap>().HasTile(tileLandedOn);

                //if found a tile at a certain tilemap at a given landing position then return the landing position
                if (hasTile)
                    return new Vector3(pseudo3DPos.x, pseudo3DPos.y, i);
                //return new Vector3(pos.x, pos.y - distFromTilemap, pos.z);
            }
        }

        //couldn't find a location
        return DefaultPosition;
    }

    /// <summary>
    /// Gets the distance from the ground, ie. tilemaps
    /// </summary>
    /// <param name="pseudo3DPos"></param>
    /// <returns></returns>
    public static float GetDistanceFromGround(Vector3 pseudo3DPos)
    {
        var distance = 0f;
        var floorsBelow = (int)Math.Truncate(pseudo3DPos.z);
        for (var i = floorsBelow; i > 0; i--) 
        {                                     
            var iLayerTilemaps = GetTilemapsByOrder(i);

            if (iLayerTilemaps.Count() <= 0)
                continue;

            foreach (var tilemap in iLayerTilemaps)
            {
                distance = pseudo3DPos.z - (float)i;
                var tileLandedOn = new Vector3Int((int)Math.Floor(pseudo3DPos.x), (int)Math.Floor(pseudo3DPos.y + i), 0);
                var hasTile = tilemap.GetComponent<Tilemap>().HasTile(tileLandedOn);
                
                if (hasTile)
                    return distance;
            }
        }
        return distance;
    }

    private static Tilemap[] GetTilemapsByOrder(int order)
    {
        var tilemaps = new List<Tilemap>();

        var grid = GameObject.FindGameObjectWithTag("Grid");

        //check each tilemap in grid to see if it is under the sorting layer sortingLayer
        if (grid != null)
        {
            foreach (Transform child in grid.transform)
            {

                if (child.gameObject.tag == "Tilemap" &&
                    child.gameObject.GetComponent<TilemapRenderer>().sortingOrder == order)
                {
                    tilemaps.Add(child.gameObject.GetComponent<Tilemap>());
                }
            }
        }
        return tilemaps.ToArray();
    }


}
