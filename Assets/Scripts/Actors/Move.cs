using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public enum Direction { UP, RIGHT, DOWN, LEFT };

public class Move : MonoBehaviour
{
    public float moveTime = 0.1f;
    public float moveCoolDown = 0.15f;
    public bool inMoveOneTile = true;
    public bool canMove = true;

    public float outOfBattleMoveSpeed = 1;

    public int moved;

    public GameObject grid;
    //public Tilemap groundTilemap;

    public Tilemap[] obstaclesTilemaps;
    public Tilemap[] floorTilemaps;
 
    protected bool inActionCooldown, _onExit = false;
    public bool isMoving;

    public void MoveOneTile(Direction dir)
    {
        inMoveOneTile = true;
        moved = moved * -1; //basically a move tracker
        int vertical = 0;
        int horizontal = 0;
        if (dir == Direction.UP)
            vertical = 1;
        else if (dir == Direction.DOWN)
            vertical = -1;
        else
            vertical = 0;
        if (dir == Direction.RIGHT)
            horizontal = 1;
        else if (dir == Direction.LEFT)
            horizontal = -1;
        else
            horizontal = 0;

        if (horizontal != 0)
            vertical = 0;
        if ((horizontal != 0 || vertical != 0) && inMoveOneTile)
        {
            StartCoroutine(actionCooldown(moveCoolDown));
            MoveOne(horizontal, vertical);
        }
        inMoveOneTile = false;
    }

    private void MoveOne(int xDir, int yDir)
    {
        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);

        bool isOnGround = false; //If the player is on the ground
        bool hasFloorTile = false; //If target Tile has a ground
        bool hasObstacleTile = false; //if target Tile has an obstacle
        bool hasUnitsTile = GetUnitOccupyingTile(targetCell) != null; //if target Tile has a unit

        foreach(var floorTilemap in floorTilemaps)
        {
            if (getCell(floorTilemap, startCell) != null)
                isOnGround = true;
        }

        foreach (var floorTilemap in floorTilemaps)
        {
            if (getCell(floorTilemap, targetCell) != null)
                hasFloorTile = true;
        }

        foreach (var obstaclesTilemap in obstaclesTilemaps)
        {
            if (getCell(obstaclesTilemap, targetCell) != null)
                hasObstacleTile = true;
        }

        //If the player starts their movement from a ground tile.
        if (isOnGround)
        {

            //If the front tile is a walkable ground tile, the player moves here.
            if (hasFloorTile && !hasObstacleTile && !hasUnitsTile)
            {
                StartCoroutine(SmoothMovement(targetCell));
            }

        }

        if (!isMoving)
            StartCoroutine(BlockedMovement(targetCell));
    }

    protected TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    protected GameObject GetUnitOccupyingTile(Vector2 cellWorldPos)
    {
        var layer = LayerMask.GetMask("BlockingLayer");
        var col = Physics2D.OverlapCircle(cellWorldPos,0.1f, layer); 
        if (col == null)
            return null;
        
        if (col.gameObject.GetComponent<SpriteRenderer>() != null &&
            col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName != null &&
            col.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Units")
        {
            return col.gameObject;
        }
        
        return null;
    }

    private IEnumerator actionCooldown(float cooldown)
    {
        inActionCooldown = true;

        //float cooldown = 0.2f;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        inActionCooldown = false;
    }

    private IEnumerator BlockedMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        isMoving = true;

        Vector3 originalPos = transform.position;

        end = transform.position + ((end - transform.position) / 3);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = (1 / (moveTime * 2));

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPos, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
    }

    private IEnumerator SmoothMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        isMoving = true;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
    }

    protected void MoveUp()
    {

        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, outOfBattleMoveSpeed * Time.deltaTime, 0);
        }

    }

    protected void MoveDown()
    {
        if (Input.GetKey("down"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, -outOfBattleMoveSpeed * Time.deltaTime, 0);
        }
    }

    protected void MoveRight()
    {
        if (Input.GetKey("right"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(outOfBattleMoveSpeed * Time.deltaTime, 0, 0);
        }
    }

    protected void MoveLeft()
    {
        if (Input.GetKey("left"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(-outOfBattleMoveSpeed * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
}
