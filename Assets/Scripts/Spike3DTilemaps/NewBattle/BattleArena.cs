using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using static Globals;

public class BattleArena
{
    public Vector2Int size;
    public TileBase tile;
    public Enemy[] battleEnemies;
    public Vector2Int[] battleEnemySpawnPoints;
    public Vector2Int playerSpawnPoint;
}


