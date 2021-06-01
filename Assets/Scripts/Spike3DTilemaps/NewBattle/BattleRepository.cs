using UnityEngine;
using System.Collections;
using static Globals;
using UnityEditor;
using UnityEngine.Tilemaps;

public static class BattleRepository
{
    public static BattleArena GetBattleInfoFromBattleArenaName(BattleArenaName battleArenaName)
    {

        switch (battleArenaName)
        {
            case BattleArenaName.BattleEvergreenTemple:
                return GetGrassyBricksBattleInfo();
            default:
                return GetDefaultBattleInfo();
        }
    }
    private static BattleArena GetGrassyBricksBattleInfo()
    {
        return new BattleArena
        {
            size = new Vector2Int(10, 6),
            tile = Resources.Load<TileBase>(GrassyBrickTilePath)
        };
    }

    private static BattleArena GetGrassyBricksTempleBattleInfo()
    {
        return new BattleArena
        {
            size = new Vector2Int(15, 10),
            tile = Resources.Load<TileBase>(GrassyBrickTilePath)
        };
    }

    private static BattleArena GetDefaultBattleInfo()
    {
        return new BattleArena
        {
            size = new Vector2Int(10, 15),
            tile = Resources.Load<TileBase>(GrassyBrickTilePath)
        };
    }
}
