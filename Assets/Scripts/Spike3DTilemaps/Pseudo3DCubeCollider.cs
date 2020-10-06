using UnityEngine;
using System.Collections;
using System;
using static Globals;

public class Pseudo3DCubeCollider : MonoBehaviour
{
    public float playerBuffer, colliderBuffer;
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;

    // Tile position is the bottom left of the tile
    public Vector2Int tilePosition;
    public int sortingOrder;

    public int xMin, xMax, yMin, yMax, zMin, zMax;
    private GameObject player;
    private Pseudo3DCubeColliderMaster colMaster;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerTag);
        colMaster = transform.parent.gameObject.GetComponent<Pseudo3DCubeColliderMaster>();
    }

    private void Update()
    {
        CheckIfCanMove(player.GetComponent<Pseudo3DPosition>().pseudo3DPosition);
        UpdateCanMoves();
    }

    public void CreatePseudo3DCubeCollider()
    {
        xMin = tilePosition.x;
        xMax = tilePosition.x + 1;
        yMin = tilePosition.y - 1;
        yMax = tilePosition.y;
        zMin = sortingOrder - 1;
        zMax = sortingOrder;
    }

    public void CheckIfCanMove(Vector3 objectPosition)
    {
        //x direction
        if ((objectPosition.x >= xMin - playerBuffer && objectPosition.x <= xMin + playerBuffer) && //or ==
            objectPosition.y >= yMin - colliderBuffer && objectPosition.y < yMax + colliderBuffer &&
            objectPosition.z >= zMin - colliderBuffer && objectPosition.z < zMax + colliderBuffer)
        {
            canMoveXPositive = false;
        }
        else
        {
            canMoveXPositive = true;
        }
        if ((objectPosition.x >= xMax - playerBuffer && objectPosition.x <= xMax + playerBuffer) && // or ==
            objectPosition.y >= yMin - colliderBuffer && objectPosition.y < yMax + colliderBuffer &&
            objectPosition.z > zMin - colliderBuffer && objectPosition.z <= zMax + colliderBuffer)
        {
            canMoveXNegative = false;
        }
        else
        {
            canMoveXNegative = true;
        }

        //TODO
        //There is a bug where the sorting order y direction gets pushed y + 1 unit for every sorting layer above 1.

        //Also! Look into why the wall tilemaps disappear before merging to master. Dunno what's up with that.

        //y direction
        if (objectPosition.x >= xMin - colliderBuffer && objectPosition.x < xMax + colliderBuffer &&
            (objectPosition.y >= yMin - playerBuffer && objectPosition.y <= yMin + playerBuffer) && //or ==
            objectPosition.z > zMin - colliderBuffer && objectPosition.z <= zMax + colliderBuffer)
        {
            canMoveYPositive = false;
        }
        else
        {
            canMoveYPositive = true;
        }
        if (objectPosition.x >= xMin - colliderBuffer && objectPosition.x < xMax - colliderBuffer &&
            (objectPosition.y >= yMax - playerBuffer && objectPosition.y <= yMax + playerBuffer) && //or ==
            objectPosition.z > zMin - colliderBuffer && objectPosition.z <= zMax + colliderBuffer)
        {
            canMoveYNegative = false;
        }
        else
        {
            canMoveYNegative = true;
        }

        //z direction
        if (objectPosition.x >= xMin - colliderBuffer && objectPosition.x < xMax + colliderBuffer &&
            objectPosition.y >= yMin - colliderBuffer && objectPosition.y < yMax + colliderBuffer &&
            (objectPosition.z >= zMin - playerBuffer && objectPosition.z <= zMin + playerBuffer)) //or ==)
        {
            canMoveZPositive = false;
        }
        else
        {
            canMoveZPositive = true;
        }
        if (objectPosition.x >= xMin - colliderBuffer && objectPosition.x < xMax + colliderBuffer &&
            objectPosition.y >= yMin - colliderBuffer && objectPosition.y < yMax + colliderBuffer &&
            (objectPosition.z >= zMax - playerBuffer && objectPosition.z <= zMax + playerBuffer)) //or ==)
        {
            canMoveZNegative = false;
        }
        else
        {
            canMoveZNegative = true;
        }
    }

    /// <summary>
    /// Updates the player's ability to move based on the player's position relative to this collider
    /// </summary>
    private void UpdateCanMoves()
    {
        //x
        if (colMaster.xpLock == null || colMaster.xpLock == this.gameObject)
        {
            if (!canMoveXPositive)
            {
                colMaster.canMoveXPositive = false;
                colMaster.xpLock = this.gameObject;
            }
            else
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
                colMaster.canMoveXNegative = true;
                colMaster.xnLock = null;
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
}
