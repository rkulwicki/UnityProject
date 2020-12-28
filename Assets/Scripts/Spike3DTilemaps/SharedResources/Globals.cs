using UnityEngine;
using System.Collections;

public static class Globals
{
    public static Vector3 DefaultPosition = new Vector3(-999, -999, 0);

    //Tags
    public static string TilemapTag = "Tilemap";
    public static string FloorAboveTag = "FloorAbove";
    public static string FloorBelowTag = "FloorBelow";
    public static string ObjectAboveTag = "ObjectAbove";
    public static string ObjectBelowTag = "ObjectBelow";
    public static string PlayerTag = "Player";
    public static string GridTag = "Grid";
    public static string MainCameraTag = "MainCamera";
    public static string BattleGridTag = "BattleGrid";
    public static string BattleTilemapTag = "BattleTilemap";
    public static string ShadowTag = "Shadow";

    //Input
    public static string XPositiveKey = "d";
    public static string XNegativeKey = "a";
    public static string YPositiveKey = "w";
    public static string YNegativeKey = "s";
    public static string ZPositiveKey = "e";
    public static string ZNegativeKey = "q";

    public static string JumpKey1 = "space";

    public static string InteractKey = "c";
    public static string StatsKey = "v";

    //UI
    public static float AspectRatio = (16 / 9);
    public static float FadeToBlackTime = 0.3f;
    public static float BeforeFadeToBlackTime = 0.6f;

    //Enemies
    public enum EnemyName
    {
        Pumpkin
    }

    //Battle Arenas
    public enum BattleArenaName
    {
        GrassyBricks,
        GrassyBricksTemple,
        Default
    }

    //Scene Names
    public enum SceneNames
    {
        BattleScene,
        GrassyBricksScene1,
        GrassyBricksScene2,
        GrassyBricksScene3,
        Default,

        EvergreenTempleTown,
        EvergreenTempleBottomPath,
        EvergreenTempleTownAqueduct,
        EvergreenTempleBottomPathAqueduct,
        EvergreenTempleClimbToTemple,
        EvergreenTempleExterior,
        FrankFranklbeeForestToTempleTown,
        FrankFranklbeeForestDeepForest
    }

    //Tile Resources
    public static string GrassyBrickTilePath = "Tiles/GrassyBricksTile";

    //Application
    public static string PlayerFileName = "/playerInfo.dat";

    //Icon Resources
    public static string EMarkPath = "Prefabs/Icons/EMark1";

    //UI Resources
    public static string BlackScreenPath = "Prefabs/UI/BlackScreen";

    //Actor Resources
    public static string PlayerPrefabPath = "Prefabs/Pseudo3DPlayer";
}
