using UnityEngine;
using System.Collections;
using System;
using static Globals;
using UnityEngine.Tilemaps;

public enum Directions3DEnum { XPOS, XNEG, YPOS, YNEG, ZPOS, ZNEG }

public class Pseudo3DCubeCollider : MonoBehaviour
{
    public float playerBuffer, colliderBuffer, zNegPlayerBuffer;
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;

    // Tile position is the bottom left of the tile
    public Vector2Int tilePosition;
    public int sortingOrder;
    public Tilemap tilemap;
    public int floorNumber;

    public float xMin, xMax, yMin, yMax, zMin, zMax;
    private GameObject player;
    private Pseudo3DCubeColliderMaster colMaster;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerTag);
        colMaster = transform.parent.gameObject.GetComponent<Pseudo3DCubeColliderMaster>();
    }

    private void Update()
    {
        //var rounded = RoundPosition(player.GetComponent<Pseudo3DPosition>().pseudo3DPosition, 0);
        CheckIfCanMove(player.GetComponent<Pseudo3DPosition>().pseudo3DPosition);
        UpdateCanMoves();
    }

    public Vector3 RoundPosition(Vector3 pos, int decimals)
    {
        return new Vector3((float)Math.Round(pos.x, decimals), (float)Math.Round(pos.y, decimals), (float)Math.Round(pos.z, decimals));
    }

    public void CreatePseudo3DCubeCollider()
    {
        xMin = tilePosition.x - colliderBuffer;
        xMax = tilePosition.x + 1 + colliderBuffer;
        yMin = tilePosition.y - 1 - colliderBuffer;
        yMax = tilePosition.y + colliderBuffer;
        zMin = sortingOrder - 1 - colliderBuffer;
        zMax = sortingOrder + colliderBuffer;
    }

    //TODO - instead of just checking for one point, check for a little cube
    public void CheckIfCanMove(Vector3 objectPosition)
    {
        //Note: playerBuffer is like the player radius.
        //x direction

        //adding the buffer to parts make it sticky :(((
        // positive direction
        if ((objectPosition.x >= xMin - playerBuffer * 2 && objectPosition.x <= xMin + playerBuffer * 2) && //or ==
            objectPosition.y >= yMin - playerBuffer && objectPosition.y < yMax + playerBuffer &&
            objectPosition.z >= zMin && objectPosition.z < zMax)
        {
            canMoveXPositive = false;
        }
        else
        {
            canMoveXPositive = true;
        }
        // negative direction
        if ((objectPosition.x >= xMax - playerBuffer * 2 && objectPosition.x <= xMax + playerBuffer * 2) && // or ==
            objectPosition.y >= yMin - playerBuffer && objectPosition.y < yMax + playerBuffer &&
            objectPosition.z > zMin && objectPosition.z <= zMax)
        {
            canMoveXNegative = false;
        }
        else
        {
            canMoveXNegative = true;
        }

        //y direction
        // positive direction
        if (objectPosition.x >= xMin - playerBuffer && objectPosition.x < xMax + playerBuffer &&
            (objectPosition.y >= yMin - playerBuffer * 4 && objectPosition.y <= yMin + playerBuffer * 4) && //or ==
            objectPosition.z > zMin && objectPosition.z <= zMax)
        {
            canMoveYPositive = false;
        }
        else
        {
            canMoveYPositive = true;
        }
        // negative direction
        if (objectPosition.x >= xMin - playerBuffer && objectPosition.x < xMax + playerBuffer &&
            (objectPosition.y >= yMax - playerBuffer * 4 && objectPosition.y <= yMax + playerBuffer * 4) && //or ==
            objectPosition.z > zMin && objectPosition.z <= zMax)
        {
            canMoveYNegative = false;
        }
        else
        {
            canMoveYNegative = true;
        }

        //z direction
        // positive direction
        if (objectPosition.x >= xMin && objectPosition.x < xMax &&
            objectPosition.y >= yMin && objectPosition.y < yMax &&
            (objectPosition.z >= zMin - playerBuffer * 2 && objectPosition.z <= zMin + playerBuffer * 2)) //or ==)
        {
            canMoveZPositive = false;
        }
        else
        {
            canMoveZPositive = true;
        }
        // negative direction
        if (objectPosition.x >= xMin && objectPosition.x < xMax &&
            objectPosition.y >= yMin && objectPosition.y < yMax &&
            (objectPosition.z >= zMax - zNegPlayerBuffer && objectPosition.z <= zMax + zNegPlayerBuffer)) //or ==)
        {
            canMoveZNegative = false;
        }
        else
        {
            canMoveZNegative = true;
        }
    }

    //TODO - When you go from a locked state to an unlocked state, there is a breif period of time where
    // the player can move and that can make you clip through shit.
    // On Unlock - FIRST check if there's another Pseudo3DCubeCollider that can take the handoff.

    /// <summary>
    /// Updates the player's ability to move based on the player's position relative to this collider
    /// </summary>
    private void UpdateCanMoves()
    {
        //x
        if (colMaster.xpLock == null || colMaster.xpLock == this.gameObject)
        {
            if (!canMoveXPositive) //lock
            {
                colMaster.canMoveXPositive = false;
                colMaster.xpLock = this.gameObject;
            }
            else //unlock
            {
                colMaster.canMoveXPositive = true;
                colMaster.xpLock = null;
            }
        }

        if (colMaster.xnLock == null || colMaster.xnLock == this.gameObject)
        {
            if (!canMoveXNegative)
            {
                colMaster.canMoveXNegative = false;
                colMaster.xnLock = this.gameObject;
            }
            else
            {
                //before we unlock, we need to check if there is another block
                //to hand the lock off to first to prevent clipping while briefly unlocked.

                //NOTE: WE NEED TO CHECK THIS vvvvvvvv
                //colMaster.xnLock == this.gameObject

                var blockTakingHandoff = HandoffCollision(Directions3DEnum.XNEG);
                if (blockTakingHandoff != null)
                {
                    colMaster.canMoveXNegative = false;
                    colMaster.xnLock = blockTakingHandoff;
                }
                else //no block to hand-off to. You're free to move!
                {
                    colMaster.canMoveXNegative = true;
                    colMaster.xnLock = null;
                }
            }
        }

        //y
        if (colMaster.ypLock == null || colMaster.ypLock == this.gameObject)
        {
            if (!canMoveYPositive)
            {
                colMaster.canMoveYPositive = false;
                colMaster.ypLock = this.gameObject;
            }
            else
            {
                colMaster.canMoveYPositive = true;
                colMaster.ypLock = null;
            }
        }

        if (colMaster.ynLock == null || colMaster.ynLock == this.gameObject)
        {
            if (!canMoveYNegative)
            {
                colMaster.canMoveYNegative = false;
                colMaster.ynLock = this.gameObject;
            }
            else
            {
                colMaster.canMoveYNegative = true;
                colMaster.ynLock = null;
            }
        }

        //z
        if (colMaster.zpLock == null || colMaster.zpLock == this.gameObject)
        {
            if (!canMoveZPositive)
            {
                colMaster.canMoveZPositive = false;
                colMaster.zpLock = this.gameObject;
            }
            else
            {
                colMaster.canMoveZPositive = true;
                colMaster.zpLock = null;
            }
        }

        if (colMaster.znLock == null || colMaster.znLock == this.gameObject)
        {
            if (!canMoveZNegative)
            {
                colMaster.canMoveZNegative = false;
                colMaster.znLock = this.gameObject;
            }
            else
            {
                colMaster.canMoveZNegative = true;
                colMaster.znLock = null;
            }
        }
    }

    //  We need this so that way we don't clip between two different blocks when there
    //is a brief moment when releasing the movement lock.
    //  When releasing the lock for a certain direction (ie. x positive), we need to first check
    //and see if there
    private GameObject HandoffCollision(Directions3DEnum dir)
    {
        TileBase t = null;
        var loc = new Vector3(0, 0, 0);
        //get z val by tilemap name
        switch (dir)
        {
            case Directions3DEnum.XPOS:
                break;
            case Directions3DEnum.XNEG:
                loc = new Vector3(tilePosition.x - 1, tilePosition.y, floorNumber);
                t = this.tilemap.GetTile(tilemap.WorldToCell(loc));
                break;
            case Directions3DEnum.YPOS:
                break;
            case Directions3DEnum.YNEG:
                break;
            case Directions3DEnum.ZPOS:
                break;
            case Directions3DEnum.ZNEG:
                break;
        }

        if(t != null) //then handoff to new pseudo3DCollider
        {
            //get pseudo3DCollider GameObject
            var handoffPseudo3DColliderObject = transform.parent.Find($"({loc.x},{loc.y},{loc.z})");
            if(handoffPseudo3DColliderObject != null)
            {
                return handoffPseudo3DColliderObject.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}