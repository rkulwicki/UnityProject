using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightBlocks : TileManager
{
    public bool testHighlight = false;
    public Vector3Int[] tileLocations;

    //TEST
    private void Update()
    {
        if (testHighlight)
        {
            HighlightTiles(tileLocations);
        }
    }
}
