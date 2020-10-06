using UnityEngine;
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

    //Input
    public static string XPositiveKey = "d";
    public static string XNegativeKey = "a";
    public static string YPositiveKey = "w";
    public static string YNegativeKey = "s";
    public static string ZPositiveKey = "e";
    public static string ZNegativeKey = "q";

    public static string JumpKey1 = "space";

    //
    public static float AspectRatio = (16/9);
}
