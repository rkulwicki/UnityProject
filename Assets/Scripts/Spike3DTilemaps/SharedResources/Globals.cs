﻿using UnityEngine;
using System.Collections;

public static class Globals
{
    public static Vector3 DefaultPosition = new Vector3(-999, -999, 0);

    //Tags
    public static string TilemapTag = "Tilemap";
    public static string FloorAboveTag = "FloorAbove";
    public static string FloorBelowTag = "FloorBelow";
    public static string PlayerTag = "Player";
    public static string GridTag = "Grid";
    public static string MainCameraTag = "MainCamera";
    public static string BattleGridTag = "BattleGrid";
    public static string BattleTilemapTag = "BattleTilemap";

    //Input
    public static string XPositiveKey = "d";
    public static string XNegativeKey = "a";
    public static string YPositiveKey = "w";
    public static string YNegativeKey = "s";
    public static string ZPositiveKey = "e";
    public static string ZNegativeKey = "q";

    public static string JumpKey1 = "space";

    //UI
    public static float AspectRatio = (16 / 9);

    //Enums
    public enum Enemy
    {
        TestEnemy1
    }
    public enum BattleArena
    {
        TestArena1
    }

    //Application
    public static string PlayerFileName = "/playerInfo.dat";
}