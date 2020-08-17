using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class WallTilemapScript : MonoBehaviour
{
    public float wallBufferUp, wallBufferDown, wallBufferMiddleDown;

    public TileBase testUpTile, testDownTile, testDownMiddleTile;

    public bool canMoveUp, canMoveDown;

    private Tilemap _wallTilemap;
    private MovementInfo _movementInfo;
    public string _sortingLayer;
    private PlayerMove _playerMove;
    private WallTilemapsFunctionality _wallTilemapFunctionality;

    public Vector3Int tileLocDownOneUnit, tileLocUpOneUnit, tileLocDownMidOneUnit;

    // Use this for initialization
    void Start()
    {
        _wallTilemap = gameObject.GetComponent<Tilemap>();
        _movementInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementInfo>();
        _sortingLayer = gameObject.GetComponent<TilemapRenderer>().sortingLayerName;
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        _wallTilemapFunctionality = GameObject.FindGameObjectWithTag("Grid").GetComponent<WallTilemapsFunctionality>();

        wallBufferUp = _wallTilemapFunctionality.wallBufferUp;
        wallBufferDown = _wallTilemapFunctionality.wallBufferDown;
        wallBufferMiddleDown = _wallTilemapFunctionality.wallBufferMiddleDown;
    }

    // Update is called once per frame
    void Update()
    {
        PreventPlayerMovement();
    }

    
    //NOTE: THE TILEMAPS ARE SET UP THE CENTER ARE INTEGERS!!!!

    //If a tilemap sets one of the "canMove" to false, then this tilemap is the only one that can set it to "true"
    private void PreventPlayerMovement()
    {
        _sortingLayer = gameObject.GetComponent<TilemapRenderer>().sortingLayerName;

        var tilemap = this.GetComponent<Tilemap>();

        //I add z to the y to get the tile appropriately.
        var adjustedGlobalTilePosUp = _movementInfo.GetPlayerGlobalTileLocation(wallBufferUp, Direction.UP);
        var adjustedGlobalTilePosDown = _movementInfo.GetPlayerGlobalTileLocation(wallBufferDown, Direction.DOWN);
        var adjustedGlobalTilePosMidDown = _movementInfo.GetPlayerGlobalTileLocation(wallBufferMiddleDown, Direction.DOWN);

        tileLocUpOneUnit = new Vector3Int(adjustedGlobalTilePosUp.x, adjustedGlobalTilePosUp.y + adjustedGlobalTilePosUp.z, 0);
        tileLocDownOneUnit = new Vector3Int(adjustedGlobalTilePosDown.x, adjustedGlobalTilePosDown.y + adjustedGlobalTilePosDown.z - 1, 0); //TODO  //fix how down logic works. Should look for no tilemap as the next tilemap down instead of a wall tile AND while being on a wall tile
        tileLocDownMidOneUnit = new Vector3Int(adjustedGlobalTilePosMidDown.x, adjustedGlobalTilePosMidDown.y - adjustedGlobalTilePosMidDown.z + 1, 0);

        testUpTile = tilemap.GetTile(tileLocUpOneUnit); //get tile one y unit above
        testDownTile = tilemap.GetTile(tileLocDownOneUnit);
        testDownMiddleTile = tilemap.GetTile(tileLocDownMidOneUnit);

        //TODO
        //Add lock functionality
        //If one tilemap says "you can't move up/down", then no other tilemap can say "yeah you can move, buddy" until
        //the tilemap with the lock says "ok, you can move again"

        if (_wallTilemapFunctionality.lockUp == this.gameObject || _wallTilemapFunctionality.lockUp == null) //Put a lock so other tilemaps don't mess with this.
        {
            if (testUpTile != null && _sortingLayer == "Wall")
            {
                _playerMove.canMoveUp = false; //lock
                _wallTilemapFunctionality.lockUp = this.gameObject;
            }
            else
            {
                _playerMove.canMoveUp = true; //unlock
                _wallTilemapFunctionality.lockUp = null;
            }
        }

        if (_wallTilemapFunctionality.lockDown == this.gameObject || _wallTilemapFunctionality.lockDown == null) //Put a lock so other tilemaps don't mess with this.
        {
            //if the next below tile is not a wall, but also you're on a wall and behind it, then you cannot move downward.
            if (testDownMiddleTile != null && testDownTile == null && _sortingLayer == "WallFront")
            {
                _playerMove.canMoveDown = false;
                _wallTilemapFunctionality.lockDown = this.gameObject;
            }
            else
            {
                _playerMove.canMoveDown = true;
                _wallTilemapFunctionality.lockDown = null;
            }
        }
    }
}
