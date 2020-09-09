using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// represents a single tile which has 3d properties
/// </summary>
public class Pseudo3DTile : TileBase
{

    public Vector2Int xBounds, yBounds, zBounds;
    public Sprite[] testTiles;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        //get tile
        var tm = GetThisTilemapGameObject(tilemap);
        //tm.GetComponent<Tilemap>().SetTile();
        return base.StartUp(position, tilemap, go);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
        //base.RefreshTile(new Vector3Int(position.x, position.y - 1, position.z), tilemap);

        //need to place another tile down below this one :)

        //for (int yd = -1; yd <= 1; yd++) { 
        //    for (int xd = -1; xd <= 1; xd++)
        //    {
        //        Vector3Int pos = new Vector3Int(position.x + xd, position.y + yd, position.z);
        //        if (HasTile(tilemap, pos))
        //            tilemap.RefreshTile(pos);
        //    }
        //}
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        return base.GetTileAnimationData(position, tilemap, ref tileAnimationData);
    }

    #region Private Methods

    private GameObject GetThisTilemapGameObject(ITilemap tm)
    {
        var grid = GameObject.FindGameObjectWithTag("Grid");
        var tmList = new List<GameObject>();
        foreach (Transform child in grid.transform)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingOrder == child.GetComponent<TilemapRenderer>().sortingOrder)
                return child.gameObject;
        }
        return null;
    }

    //todo
    private bool HasTile(ITilemap tilemap, Vector3Int position)
    {
        return true;
    }

    #endregion

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a pseudo3dTile Asset
    [MenuItem("Assets/Create/Pseudo3DTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Pseudo 3D Tile", "New Pseudo 3D Tile", "Asset", "Save Pseudo 3D Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<Pseudo3DTile>(), path);
    }
#endif
}
