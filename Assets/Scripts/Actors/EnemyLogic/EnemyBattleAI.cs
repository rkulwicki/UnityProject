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

    private EnemyStats _enemyStatsReference;
    protected GameObject player;
    protected bool inMoveTowardsActor;
    protected bool inMoveTowardsActorWIP;

    void Start()
    {
        _enemyStatsReference = gameObject.GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        grid = GameObject.FindGameObjectWithTag("Grid");
        groundTilemap = grid.transform.Find("Floor").gameObject.GetComponent<Tilemap>();
        obstaclesTilemap = grid.transform.Find("Obstacles").gameObject.GetComponent<Tilemap>();
        inMoveTowardsActor = false;
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

    // TODO
    //=== enemy methods ===
    protected bool CanAttackInRange() //todo
    {
        return true;
    }

    //TODO!!!!!!!!!!!!!!!
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

    //TODO!!!!!!!!!!!!!!! HOW DO I MAKE THIS WORK AHHHHHHH
    //public IEnumerator MoveTowardsActor(GameObject myActor, GameObject otherActor, int stepsToMove)
    //{
    //    if(stepsToMove > 0 && inMoveTowardsActor)
    //    {
    //        StartCoroutine(MoveTowardsActor(myActor, otherActor));
    //        stepsToMove--;
    //    }
    //    else
    //    {
    //        yield return null;
    //    }
    //}

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

    protected bool IsTileOpen(Vector3Int target)
    {
        if (GetUnitOccupyingTile(new Vector2(target.x, target.y)) != null) return false; //check for units

        if (GetCell(groundTilemap, new Vector2(target.x, target.y)) != null) return false; //check for obstacles

        return true;

    }

    protected GameObject GetUnitOccupyingTile(Vector2 cellWorldPos)
    {
        var col = Physics2D.OverlapCircle(cellWorldPos, 0.1f);
        if (col == null)
            return null;

        if (col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName != null &&
            col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Units")
        {
            return col.gameObject;
        }

        return null;
    }

    private TileBase GetCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
}