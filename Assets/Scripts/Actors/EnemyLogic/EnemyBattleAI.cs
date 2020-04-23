using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBattleAI : Move
{

    //EnemeyLogic class will contain a base for how enemies will behave
    //(1) Movement Patterns
    //(2) Attack Patterns

    protected EnemyStats enemyStats;
    protected GameObject player;
    protected bool inMoveTowardsActor;
    protected bool inMoveTowardsActorWIP;

    protected int stepsCounter = 0;
    protected EnemyBattleActions enemyActions;

    protected BattleManager battleManager;

    protected void EnemyBattleAIStart()
    {
        enemyActions = gameObject.AddComponent<EnemyBattleActions>();
        enemyStats = gameObject.GetComponent<EnemyStats>();
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        grid = GameObject.FindGameObjectWithTag("Grid");
        groundTilemap = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        obstaclesTilemap = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
        inMoveTowardsActor = false;

        //battle manager
    }

    protected void CheckDead()
    {
        if (gameObject.GetComponent<EnemyStats>().currentHP <= 0)  //dead :(
        {
            gameObject.SetActive(false);

            //remove from enemiesInvolved array
            var battleManager = GameObject.FindGameObjectWithTag("BattleManager");
            var enemiesInvolved = battleManager.GetComponent<BattleManager>().enemiesInvolved;
            System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(enemiesInvolved);
            list.Remove(gameObject);
            battleManager.GetComponent<BattleManager>().enemiesInvolved = list.ToArray();
        }
    }

    //TODO!!!!!!!!!!!!!!!
    //works but an be improved
    /// <summary>
    /// Moves linearly towards the actor given.
    /// </summary>
    /// <param name="myActor"></param>
    /// <param name="otherActor"></param>
    protected void MoveTowardsActor(GameObject myActor, GameObject otherActor)
    {
        inMoveTowardsActor = true;

        Vector3Int otherActorPos = new Vector3Int(Convert.ToInt32(otherActor.transform.position.x),
                                                Convert.ToInt32(otherActor.transform.position.y),
                                                Convert.ToInt32(otherActor.transform.position.z));
        Vector3Int myActorPos = new Vector3Int(Convert.ToInt32(myActor.transform.position.x),
                                        Convert.ToInt32(myActor.transform.position.y),
                                        Convert.ToInt32(myActor.transform.position.z));

        var posDif = otherActorPos - myActorPos;

        //note: may need to move this into a new function and make this recursive.
        //go up, go right, go down, go left
        //directions to go 
        if (posDif.x == 0 && posDif.y > 0) //12. up
        {
            //try move up.
            MoveOneTile(Direction.UP);
        }
        else if (posDif.x > 0 && posDif.y > 0) //1-2. up/right
        {

            if (Math.Abs(posDif.x) > Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.RIGHT);
            }
            else if (Math.Abs(posDif.x) < Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.UP);
            }
            else
            {
                //rand between up or right 
                MoveOneTile(Direction.RIGHT);//temp
            }
        }
        else if (posDif.x > 0 && posDif.y == 0) //3. right
        {
            MoveOneTile(Direction.RIGHT);
        }
        else if (posDif.x > 0 && posDif.y < 0) //4-5. down/right
        {

            if (Math.Abs(posDif.x) > Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.RIGHT);
            }
            else if (Math.Abs(posDif.x) < Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.DOWN);
            }
            else
            {
                //rand between up or right 
                MoveOneTile(Direction.RIGHT);//temp
            }
        }
        else if (posDif.x == 0 && posDif.y < 0) //6. down
        {
            MoveOneTile(Direction.DOWN);
        }
        else if (posDif.x < 0 && posDif.y < 0) //7-8. down/left
        {
            if (Math.Abs(posDif.x) > Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.LEFT);
            }
            else if (Math.Abs(posDif.x) < Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.DOWN);
            }
            else
            {
                //rand between up or right
                MoveOneTile(Direction.LEFT);//temp
            }
        }
        else if (posDif.x < 0 && posDif.y == 0) //9. left
        {
            MoveOneTile(Direction.LEFT);
        }
        else if (posDif.x < 0 && posDif.y > 0) //10-11. up/left
        {
            if (Math.Abs(posDif.x) > Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.LEFT);
            }
            else if (Math.Abs(posDif.x) < Math.Abs(posDif.y))
            {
                MoveOneTile(Direction.UP);
            }
            else
            {
                //rand between up or right 
                MoveOneTile(Direction.LEFT);//temp
            }
        }

    }
    public bool MoveTowardsActor(GameObject me, GameObject other, int steps)
    {
        if (isMoving || inActionCooldown || _onExit || !canMove) return true; //moving. don't try to move again

        MoveTowardsActor(this.gameObject, player);
        stepsCounter++;
        if (stepsCounter >= steps)
        {
            stepsCounter = 0;
            return false; // Done.
        }
        return true;
    }

    //TODO:
    //private Direction? DecideDirection(int x, int y)
    //{
    //    if (Math.Abs(x) > Math.Abs(y))
    //    {
    //        if(x > 0)
    //            return Direction.RIGHT;
    //        if (x < 0)
    //            return Direction.LEFT;
    //        return null;
    //    }
    //    else if (Math.Abs(x) < Math.Abs(y))
    //    {
    //        if (y > 0)
    //            return Direction.UP;
    //        if (y < 0)
    //            return Direction.DOWN;
    //        return null;
    //    }
    //    //what about if both 0?
    //    else
    //    {
    //        //rand between up or right 
    //    }
    //}

        /// <summary>
        /// Checks to see a tile is open by checking for a unit, and if it there is an obstacles tile
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
    protected bool IsTileOpen(Vector3Int target)
    {
        if (GetUnitOccupyingTile(new Vector2(target.x, target.y)) != null) return false; //check for units

        if (GetCell(obstaclesTilemap, new Vector2(target.x, target.y)) != null) return false; //check for obstacles

        return true;
    }


    private TileBase GetCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    protected GameObject GetPlayerInRange(Vector3Int[] tiles)
    {
        var units = GetUnitsInRange(tiles);
        var player = GetObjectInArrayWithTag(units, "Player");
        return player;
    }

    protected GameObject[] GetUnitsInRange(Vector3Int[] tiles)
    {
        List<GameObject> list = new List<GameObject>();
        foreach(var tile in tiles)
        {
            var targetGameObject = GetUnitOccupyingTile(new Vector2(tile.x, tile.y));
            if(targetGameObject != null)
            {
                list.Add(targetGameObject);
            }
        }
        return list.ToArray();
    }

    protected GameObject GetObjectInArrayWithTag(GameObject[] objs, string tag)
    {
        foreach(var obj in objs)
        {
            if (obj.tag == tag)
            {
                return obj;
            }
        }
        return null;
    }

    protected Vector3Int[] Reposition(Vector3Int[] localPos, Vector3Int center)
    {
        var globalPos = new Vector3Int[localPos.Length];
        for (int i = 0; i < localPos.Length; i++)
        {
            globalPos[i] = localPos[i] + center;
        }
        return globalPos;
    }

    protected Vector3Int ConvertV3ToV3Int(Vector3 vec)
    {
        return new Vector3Int(Convert.ToInt32(vec.x),
                                Convert.ToInt32(vec.y),
                                Convert.ToInt32(vec.z));
    }
}