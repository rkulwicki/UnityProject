using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class WallTilemapScript : MonoBehaviour
{
    public float wallBuffer;

    public TileBase testUp, testDown;

    public bool canMoveUp, canMoveDown;

    private Tilemap _wallTilemap;
    private MovementInfo _movementInfo;
    public string _sortingLayer;
    private PlayerMove _playerMove;
    private WallTilemapsFunctionality _wallTilemapFunctionality;

    public Vector3Int tileLocDownOneUnit, tileLocUpOneUnit;

    // Use this for initialization
    void Start()
    {
        _wallTilemap = gameObject.GetComponent<Tilemap>();
        _movementInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementInfo>();
        _sortingLayer = gameObject.GetComponent<TilemapRenderer>().sortingLayerName;
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        _wallTilemapFunctionality = GameObject.FindGameObjectWithTag("Grid").GetComponent<WallTilemapsFunctionality>();

        wallBuffer = _wallTilemapFunctionality.wallBuffer;
    }

    // Update is called once per frame
    void Update()
    {
        PreventPlayerMovement();
        //_playerMove.canMoveUp = canMoveUp;
        //_playerMove.canMoveDown = true;//canMoveDown; //TODO - Down needs work
        //_playerMove.canMoveRight = true;
        //_playerMove.canMoveLeft = true;
    }

    //If a tilemap sets one of the "canMove" to false, then this tilemap is the only one that can set it to "true"
    private void PreventPlayerMovement()
    {
        _sortingLayer = gameObject.GetComponent<TilemapRenderer>().sortingLayerName;

        var tilemap = this.GetComponent<Tilemap>();

        //I add z to the y to get the tile appropriately.
        var adjustedGlobalTilePosUp = _movementInfo.GetPlayerGlobalTileLocation(wallBuffer, Direction.UP);
        var adjustedGlobalTilePosDown = _movementInfo.GetPlayerGlobalTileLocation(wallBuffer, Direction.DOWN);
        tileLocUpOneUnit = new Vector3Int(adjustedGlobalTilePosUp.x, adjustedGlobalTilePosUp.y + adjustedGlobalTilePosUp.z, 0);


        TODO 
            //fix how down logic works. Should look for no tilemap as the next tilemap down instead of a wall tile AND while being on a wall tile

        tileLocDownOneUnit = new Vector3Int(adjustedGlobalTilePosDown.x, adjustedGlobalTilePosDown.y + adjustedGlobalTilePosDown.z - 1, 0);




        testUp = tilemap.GetTile(tileLocUpOneUnit); //get tile one y unit above
        testDown = tilemap.GetTile(tileLocDownOneUnit);


        //TODO
        //Add lock functionality
        //If one tilemap says "you can't move up/down", then no other tilemap can say "yeah you can move, buddy" until
        //the tilemap with the lock says "ok, you can move again"

        if (_wallTilemapFunctionality.lockUp == this.gameObject || _wallTilemapFunctionality.lockUp == null) //Put a lock so other tilemaps don't mess with this.
        {
            if (testUp != null && _sortingLayer == "Wall")
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

        //if (testDown != null && _sortingLayer == "WallFront") //if on wallTile && next tile down is null
        //{
        //    canMoveDown = false;
        //}
        //else
        //{
        //    canMoveDown = true;
        //}
        
    }
}
