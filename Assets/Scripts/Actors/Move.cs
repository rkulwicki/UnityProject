using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public enum Direction { UP, RIGHT, DOWN, LEFT };

public class Move : MonoBehaviour
{
    public float moveTime = 0.1f;
    public float moveCoolDown = 0.15f;
    public bool canMoveOneTile = true;
    public bool canMove = true;

    public int moved;

    public GameObject grid;
    public Tilemap groundTilemap;
    public Tilemap obstaclesTilemap;

    protected bool _onCooldown, _onExit = false;
    public bool isMoving;

    public void MoveOneTile(Direction dir)
    {
        canMoveOneTile = true;
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
        if ((horizontal != 0 || vertical != 0) && canMoveOneTile)
        {
            StartCoroutine(actionCooldown(moveCoolDown));
            MoveOne(horizontal, vertical);
        }
        canMoveOneTile = false;
    }

    private void MoveOne(int xDir, int yDir)
    {
        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);

        bool isOnGround = getCell(groundTilemap, startCell) != null; //If the player is on the ground
        bool hasGroundTile = getCell(groundTilemap, targetCell) != null; //If target Tile has a ground
        bool hasObstacleTile = getCell(obstaclesTilemap, targetCell) != null; //if target Tile has an obstacle

        //If the player starts their movement from a ground tile.
        if (isOnGround)
        {

            //If the front tile is a walkable ground tile, the player moves here.
            if (hasGroundTile && !hasObstacleTile)
            {
                StartCoroutine(SmoothMovement(targetCell));
            }

        }

        if (!isMoving)
            StartCoroutine(BlockedMovement(targetCell));
    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    private IEnumerator actionCooldown(float cooldown)
    {
        _onCooldown = true;

        //float cooldown = 0.2f;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        _onCooldown = false;
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
}
